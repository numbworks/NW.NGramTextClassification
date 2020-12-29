<#

    Title: NW_Builder (build.ps1)
    Author: numbworks@gmail.com
    Last Update: 27.12.2020
    Description: 

        Builds all the projects listed into a solution file and places the artifact(s) in the "artifacts" sub-folder.
        
        The script expects :
            - a solution directory containing only one .sln file
            - an "artifacts" sub-folder to place artifacts into
            - dotnet.exe correctly installed on the machine

        Worth to mention:
            - the artifacts will be NuGet packages
            - functions provide basic logging functionality
            - the script can be run both in VSCode and in a shell/Windows Terminal

#>

# Functions
function Assert-ThatDotnetIsInstalled
{

    try 
    {

        . dotnet | Out-Null
        Write-Host -Object "'dotnet' is installed."

        return $true 

    } 
    catch { 
        
        Write-Host -Object "'dotnet' is not installed."
        return $false 
    
    }

}
function Get-CurrentDirectory() {

    [string]$currentDir = $null
    if ($PSISE) { 
        $currentDir = (Split-Path -Path $psISE.CurrentFile.FullPath) 
    }
    elseif ($profile.Contains("VSCode")) {
        $currentDir = (Split-Path $PSEditor.GetEditorContext().CurrentFile.Path) 
    }
    elseif (-not $PSScriptRoot) {
        $currentDir = (Get-ChildItem | ForEach-Object { $_.DirectoryName } | Select-Object -Unique)
    }
    else { 
        $currentDir = $PSScriptRoot 
    }

    return [System.IO.DirectoryInfo]::new($currentDir)

}
function Get-ArtifactsSubfolder
{

    param(
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$SolutionDir
    )

    [System.IO.DirectoryInfo]$artifactsDir = [System.IO.Path]::Combine($SolutionDir, "artifacts")
    if ($artifactsDir.Exists.Equals($false)) 
    {
        Write-Host -Object "The artifacts sub-directory doesn't exist in the provided solution directory."
        return $null
    }

    Write-Host -Object "The artifacts sub-directory does exist in the provided solution directory."

    return $artifactsDir

}
function New-Artifact
{

    param(
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$SolutionDir,
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$ArtifactsDir
    )
    
    try {

        if ($SolutionDir.Exists.Equals($false))
        {
            Write-Host -Object "The provided solution directory doesn't exist: '$($SolutionDir.FullName)'."
            return $false
        }
        if ($ArtifactsDir.Exists.Equals($false))
        {
            Write-Host -Object "The provided artifacts directory doesn't exist: '$($ArtifactsDir.FullName)'."
            return $false
        }

        Write-Host -Object "The following solution directory has been provided: '$($SolutionDir.FullName)'."       
        Write-Host -Object "The following artifacts directory has been provided: '$($ArtifactsDir.FullName)'."   

        [System.IO.FileInfo[]]$solutionFiles = (Get-ChildItem -Path "$SolutionDir\*" -Include @("*.sln"))
        if ($solutionFiles.Count.Equals(1).Equals($false))
        {
            Write-Host -Object "An unexpected amount of solution files have been found in the current directory."  
            return $false  
        }

        [System.IO.FileInfo]$solutionFileName = $solutionFiles[0]
        Write-Host -Object "The following solution file has been found: '$($solutionFileName.Name)'."  
        Write-Host -Object "Creating NuGet package(s) using 'dotnet pack'..." 
        Write-Host -Object ""
        
        dotnet pack $solutionFileName --output $ArtifactsDir
        
        Write-Host -Object ""
        Write-Host -Object "Exit code is: '$($LASTEXITCODE)'."

        if ($LASTEXITCODE.Equals(0))
        {
            return $true
        }
        else 
        {
            return $false
        }

    }
    catch 
    {
        
        Write-Host -Object $_.Exception.Message
        return $false

    }  

}
function Get-ArtifactNames
{

    param(
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$ArtifactsDir
    )    

    if ($ArtifactsDir.Exists.Equals($false))
    {
        Write-Host -Object "The provided artifacts directory doesn't exist: '$($ArtifactsDir.FullName)'."
        return $null
    }

    [string[]]$artifactNames = (Get-ChildItem -Path "$ArtifactsDir\*" -Include @("*.nupkg") | ForEach-Object { $_.Name} )
    Write-Host -Object "'$($artifactNames.Count)' NuGet packages have been found in the provided artifacts sub-directory."

    return $artifactNames

}
function Invoke-BuildProcess
{
    
    param(
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$SolutionDir
    )
   
    if ($SolutionDir.Exists.Equals($false))
    {
        Write-Host -Object "The provided solution directory doesn't exist: '$($SolutionDir.FullName)'."
        return $false
    }    

    Write-Host -Object "Build process started for '$($SolutionDir.FullName)'."   

    [System.IO.DirectoryInfo]$artifactsDir = (Get-ArtifactsSubfolder -SolutionDir $solutionDir)
    if (-not $artifactsDir)
    {
        Write-Host -Object "Aborting."
        break
    }
    
    $buildStatus = (New-Artifact -SolutionDir $solutionDir -ArtifactsDir $artifactsDir)
    if ($buildStatus.Equals($false))
    {
        Write-Host -Object "Aborting."
        break
    }
    [string[]]$artifactNames = (Get-ArtifactNames -ArtifactsDir $artifactsDir)
    if ($artifactNames)
    {
        Write-Host -Object "The files are:"
        $artifactNames
    }
    
    Write-Host -Object "Build process completed."

}

# Main
Clear-Host
if ($(Assert-ThatDotnetIsInstalled).Equals($false))
{
    Write-Host -Object "Aborting."
    break
}

[System.IO.DirectoryInfo]$solutionDir = Get-CurrentDirectory
Invoke-BuildProcess -SolutionDir $solutionDir