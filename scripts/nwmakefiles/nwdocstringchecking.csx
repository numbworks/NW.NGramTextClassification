#!/usr/bin/env dotnet-script
#r "nuget: Microsoft.CodeAnalysis.CSharp, 4.8.0"

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

int missingCount = RunValidation();
HandleValidationResult(missingCount);

private void HandleValidationResult(int missingCount)
{
    if (missingCount > 0)
    {
        Console.WriteLine($"\nValidation failed: Found {missingCount} public member(s) missing documentation.");
        Environment.Exit(1); 
    }
    else
    {
        Console.WriteLine("All production public methods and constructors are properly documented!");
        Environment.Exit(0);
    }
}
private int RunValidation()
{

    IEnumerable<string> projectFiles;

    if (Args.Count > 0 && !string.IsNullOrWhiteSpace(Args[0]))
    {
        string slnPath = Args[0];
        if (!File.Exists(slnPath))
        {
            Console.WriteLine($"Error: Solution file not found at '{slnPath}'");
            Environment.Exit(1);
        }

        Console.WriteLine($"Parsing solution: {slnPath}");
        projectFiles = GetProjectsFromSolution(slnPath);
    
    }
    else
        projectFiles = Directory.GetFiles(".", "*.csproj", SearchOption.AllDirectories);

    int totalMissing = 0;

    foreach (string projFile in projectFiles)
    {
        if (!File.Exists(projFile) || IsBuildArtifact(projFile) || IsTestProject(projFile))
            continue;

        string projDirectory = Path.GetDirectoryName(projFile);
        IEnumerable<string> csFiles = Directory.GetFiles(projDirectory, "*.cs", SearchOption.AllDirectories)
            .Where(f => !IsBuildArtifact(f));

        foreach (string file in csFiles)
            totalMissing += AnalyzeFile(file);

    }

    return totalMissing;
}
private IEnumerable<string> GetProjectsFromSolution(string solutionFilePath)
{

    // Standard .sln project declaration format: Project("{GUID}") = "Name", "RelPath.csproj", "{GUID}"

    List<string> projectPaths = new List<string>();
    string slnDirectory = Path.GetDirectoryName(solutionFilePath);
    string[] lines = File.ReadAllLines(solutionFilePath);

    Regex projectRegex = new Regex(@"Project\([^)]+\)\s*=\s*""[^""]+"",\s*""([^""]+\.csproj)""", RegexOptions.Compiled);

    foreach (string line in lines)
    {
        Match match = projectRegex.Match(line);
        if (match.Success)
        {
            string relativePath = match.Groups[1].Value;
            relativePath = relativePath.Replace('\\', Path.DirectorySeparatorChar);
            
            string fullPath = Path.GetFullPath(Path.Combine(slnDirectory, relativePath));
            projectPaths.Add(fullPath);
        }
    }

    return projectPaths;
}
private bool IsBuildArtifact(string path)
{
    return path.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}") || 
           path.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}");
}
private bool IsTestProject(string projectFilePath)
{

    try
    {
        XDocument xml = XDocument.Load(projectFilePath);
        return xml.Descendants("IsTestProject")
            .Any(x => x.Value.Trim().Equals("true", StringComparison.OrdinalIgnoreCase));
    }
    catch
    {
        return false;
    }

}
private int AnalyzeFile(string filePath)
{

    int missingInFile = 0;
    string code = File.ReadAllText(filePath);
    SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
    SyntaxNode root = tree.GetRoot();

    IEnumerable<BaseMethodDeclarationSyntax> publicMembers = root.DescendantNodes()
        .OfType<BaseMethodDeclarationSyntax>()
        .Where(m => m.Modifiers.Any(SyntaxKind.PublicKeyword) && m is not OperatorDeclarationSyntax);

    foreach (BaseMethodDeclarationSyntax member in publicMembers)
    {
        if (ShouldSkipMember(member))
            continue;

        if (IsMissingDocumentation(member))
        {
            FileLinePositionSpan lineSpan = GetIdentifierLocation(member).GetLineSpan();
            string memberName = GetMemberName(member);
            
            Console.WriteLine($"{filePath}({lineSpan.StartLinePosition.Line + 1}): Public member '{memberName}' is missing XML summary/inheritdoc.");
            missingInFile++;
        }
    }

    return missingInFile;
}
private bool ShouldSkipMember(BaseMethodDeclarationSyntax member)
{

    if (member.Modifiers.Any(SyntaxKind.OverrideKeyword))
        return true;

    if (member is MethodDeclarationSyntax)
    {
        ClassDeclarationSyntax parentClass = member.Ancestors().OfType<ClassDeclarationSyntax>().FirstOrDefault();
        if (parentClass != null && ClassHasInheritDoc(parentClass))
            return true;
    }

    return false;

}
private bool ClassHasInheritDoc(ClassDeclarationSyntax classDeclaration)
{

    SyntaxTriviaList classTrivia = classDeclaration.GetLeadingTrivia();
    IEnumerable<DocumentationCommentTriviaSyntax> classXmlComments = classTrivia
        .Select(t => t.GetStructure())
        .OfType<DocumentationCommentTriviaSyntax>();
    
    return classXmlComments.Any(comment => 
        comment.Content.OfType<XmlEmptyElementSyntax>().Any(e => e.Name.ToString() == "inheritdoc") ||
        comment.Content.OfType<XmlElementSyntax>().Any(e => e.StartTag.Name.ToString() == "inheritdoc")
    );

}
private bool IsMissingDocumentation(BaseMethodDeclarationSyntax member)
{

    SyntaxTriviaList trivia = member.GetLeadingTrivia();
    IEnumerable<DocumentationCommentTriviaSyntax> xmlComments = trivia
        .Select(t => t.GetStructure())
        .OfType<DocumentationCommentTriviaSyntax>();
    
    bool hasSummary = false;
    bool hasLocalInheritDoc = false;

    foreach (DocumentationCommentTriviaSyntax comment in xmlComments)
    {
        if (comment.Content.OfType<XmlElementSyntax>().Any(e => e.StartTag.Name.ToString() == "summary"))
            hasSummary = true;

        if (comment.Content.OfType<XmlEmptyElementSyntax>().Any(e => e.Name.ToString() == "inheritdoc"))
            hasLocalInheritDoc = true;
    }

    return !hasSummary && !hasLocalInheritDoc;
}
private Location GetIdentifierLocation(BaseMethodDeclarationSyntax member)
{
    if (member is MethodDeclarationSyntax method) 
        return method.Identifier.GetLocation();
    
    if (member is ConstructorDeclarationSyntax ctor) 
        return ctor.Identifier.GetLocation();
    
    if (member is OperatorDeclarationSyntax op) 
        return op.OperatorToken.GetLocation();
    
    return member.GetLocation();

}
private string GetMemberName(BaseMethodDeclarationSyntax member)
{
    if (member is MethodDeclarationSyntax method) 
        return method.Identifier.Text;
    
    if (member is ConstructorDeclarationSyntax ctor) 
        return $"{ctor.Identifier.Text} (Constructor)";
    if (member is OperatorDeclarationSyntax op) 
        return $"operator {op.OperatorToken.Text}";
    
    return "Unknown Member";

}