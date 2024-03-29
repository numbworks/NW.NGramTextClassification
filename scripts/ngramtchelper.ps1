<#

    Title: ngramtchelper
    Author: numbworks@gmail.com
    Last Update: 25.12.2022
    Description: 

        Please check: docs/Documentation-NW.NGramTextClassificationScripts.md

#>

# Variables
[System.IO.DirectoryInfo]$workingFolder = [System.IO.DirectoryInfo]::new("C:\wf-ngramtc")
[string]$datasetFileName = "dataset.csv"
[string]$delimiter = ","
[string]$labelColumnName = "Category"
[string]$textColumnName = "Message"
[string]$labeledExamplesFileName = "labeledexamples.json"
[string]$textSnippetsFileName = "textsnippets.json"

# Classes
class Logger {

    # Fields
    # Properties
    # Constructors
    hidden Logger() { }
    
    # Methods
    static [void] Log([string]$message) {

        Write-Host $("{0} {1}" -f $([Logger]::GetTimeStamp()), $message)
    
    }
    static hidden [string] GetTimeStamp() {

        return $(Get-Date -Format "[yyyy-MM-dd HH:mm]")

    }

}
class Validator {

    # Fields
    # Properties
    # Constructors
    hidden Validator() { }

    # Methods
    static [void] ThrowsIfStringNullOrEmpty([string]$str, [string]$variableName)
    {

        if ([string]::IsNullOrWhiteSpace($str)) {

            throw [string]::Format("'{0}' can't be empty or whitespace.", $variableName)

        }  

    }
    static [void] ThrowsIfDirectoryDoesntExist([string]$fullPath)
    {

        [System.IO.DirectoryInfo]$directory = [System.IO.DirectoryInfo]::new($fullPath)

        if ($directory.Exists.Equals($false)) {

            throw [string]::Format("'{0}' doesn't exist.", $fullPath)
            
        }

    }
    static [void] ThrowsIfFileDoesntExist([string]$fullPath)
    {

        [System.IO.FileInfo]$file = [System.IO.FileInfo]::new($fullPath)

        if ($file.Exists.Equals($false)) {

            throw [string]::Format("'{0}' doesn't exist.", $fullPath)
            
        }

    }

}
class LabeledExample {

    # Fields
    # Properties
    [string]$Label
    [string]$Text

    # Constructors
    LabeledExample([string]$label, [string]$text)
    {

        [Validator]::ThrowsIfStringNullOrEmpty($label, "label")
        [Validator]::ThrowsIfStringNullOrEmpty($text, "text") 

        $this.Label = $label
        $this.Text = $text

    }

    # Methods

}
class TextSnippet {

    # Fields
    # Properties
    [string]$Text

    # Constructors
    TextSnippet([string]$text)
    {

        [Validator]::ThrowsIfStringNullOrEmpty($text, "text") 

        $this.Text = $text

    }

    # Methods

}

# Functions
function Get-File {

    [OutputType([System.IO.FileInfo])]
    param(
	    [parameter(Mandatory=$true)] [System.IO.DirectoryInfo]$Folder,
        [parameter(Mandatory=$true)] [string]$FileName,
        [parameter(Mandatory=$false)] [bool]$SkipExistanceCheck = $true
    )  

    [string]$fullPath = [System.IO.Path]::Combine($Folder, $FileName)
    [System.IO.FileInfo]$file = [System.IO.FileInfo]::new($fullPath)

    if ($SkipExistanceCheck -eq $false -and $file.Exists.Equals($false)) {

        throw "The file doesn't exist: '{$fullPath}'."
        
    }

    return $file

}
function Convert-CSVToLabeledExamples {

    [OutputType([System.Collections.Generic.List[LabeledExample]])]
    param(
	    [parameter(Mandatory=$true)] [System.IO.FileInfo]$File,
        [parameter(Mandatory=$true)] [string]$Delimiter,
        [parameter(Mandatory=$true)] [string]$LabelColumnName,
        [parameter(Mandatory=$true)] [string]$TextColumnName
    )

    [System.Array]$csvLines = 
        $(Import-Csv -Path $File -Delimiter $Delimiter | 
            Select-Object -Property @{Name='Label'; Expression=$LabelColumnName}, @{Name='Text'; Expression=$TextColumnName})

    [System.Collections.Generic.List[LabeledExample]]$labeledExamples = [System.Collections.Generic.List[LabeledExample]]::new()
    for ( $i = 0; $i -lt $csvLines.Count; $i++ ) { 

        [LabeledExample]$labeledExample = [LabeledExample]::new($csvLines[$i].Label, $csvLines[$i].Text)
        $labeledExamples.Add($labeledExample)

    }

    return $labeledExamples

}
function Convert-LabeledExamplesToTextSnippets {

    [OutputType([System.Collections.Generic.List[TextSnippet]])]
    param(
	    [parameter(Mandatory=$true)] [System.Collections.Generic.List[LabeledExample]]$LabeledExamples
    )    

    [System.Collections.Generic.List[TextSnippet]]$textSnippets = [System.Collections.Generic.List[TextSnippet]]::new()
    for ( $i = 0; $i -lt $LabeledExamples.Count; $i++ ) { 
    
        [TextSnippet]$textSnippet = [TextSnippet]::new($LabeledExamples[$i].Text)
        $textSnippets.Add($textSnippet)
    
    }
    
    return $textSnippets

}
function Convert-LabeledExamplesToJson {

    [OutputType([string])]
    param(
	    [parameter(Mandatory=$true)] [System.Collections.Generic.List[LabeledExample]]$LabeledExamples
    )   

    [string]$json = $(ConvertTo-Json -InputObject $LabeledExamples)

    return $json

}
function Convert-TextSnippetsToJson {

    [OutputType([string])]
    param(
	    [parameter(Mandatory=$true)] [System.Collections.Generic.List[TextSnippet]]$TextSnippets
    )   

    [string]$json = $(ConvertTo-Json -InputObject $TextSnippets)

    return $json

}
function New-TextFile {

    [OutputType([void])]
    param(
	    [parameter(Mandatory=$true)] [System.IO.FileInfo]$File,
		[parameter(Mandatory=$true)] [string]$Content
    )

	if (Test-Path -Path $File) {

		Remove-Item -Path $File        
	
	}
	
    [void]$(New-Item -Path $File -ItemType File)
    [void]$(Add-Content -Path $File -Value $Content)

}
function Invoke-Script {

    [OutputType([System.Void])]
    param()

    $ErrorActionPreference = "Stop"

    try { 

        [Logger]::Log("Beginning processing the provided dataset...")

        [Logger]::Log("Validating the provided variables...")

        [Validator]::ThrowsIfDirectoryDoesntExist($workingFolder)
        [Validator]::ThrowsIfStringNullOrEmpty($datasetFileName, "datasetFileName")
        [Validator]::ThrowsIfStringNullOrEmpty($delimiter, "delimiter")
        [Validator]::ThrowsIfStringNullOrEmpty($labelColumnName, "labelColumnName")
        [Validator]::ThrowsIfStringNullOrEmpty($textColumnName, "textColumnName")
        [Validator]::ThrowsIfStringNullOrEmpty($labeledExamplesFileName, "labeledExamplesFileName")
        [Validator]::ThrowsIfStringNullOrEmpty($textSnippetsFileName, "textSnippetsFileName")

        [Logger]::Log("The provided variables have been validated.")

        [Logger]::Log([string]::Format("WorkingFolder: '{0}'.", $workingFolder))
        [Logger]::Log([string]::Format("DatasetFileName: '{0}'.", $datasetFileName))
        [Logger]::Log([string]::Format("Delimiter: '{0}'.", $delimiter))
        [Logger]::Log([string]::Format("LabelColumnName: '{0}'.", $labelColumnName))
        [Logger]::Log([string]::Format("TextColumnName: '{0}'.", $textColumnName))
        [Logger]::Log([string]::Format("LabeledExamplesFileName: '{0}'.", $labeledExamplesFileName))
        [Logger]::Log([string]::Format("TextSnippetsFileName: '{0}'.", $textSnippetsFileName))

        [Logger]::Log("Loading the provided dataset...")

        [System.IO.FileInfo]$datasetFile = $(Get-File -Folder $workingFolder -FileName $datasetFileName)
        [Validator]::ThrowsIfFileDoesntExist($datasetFile.FullName)

        [Logger]::Log("The provided dataset has been loaded.")

        [System.Collections.Generic.List[LabeledExample]]$labeledExamples = 
            $(Convert-CSVToLabeledExamples -File $datasetFile -Delimiter $delimiter -LabelColumnName $labelColumnName -TextColumnName $textColumnName)
        [System.Collections.Generic.List[TextSnippet]]$textSnippets = 
            $(Convert-LabeledExamplesToTextSnippets -LabeledExamples $labeledExamples)

        [Logger]::Log([string]::Format("The provided dataset has been converted to '{0}' LabeledExamples and TextSnippets objects.", $labeledExamples.Count))                     
        
        [System.IO.FileInfo]$labeledExamplesFile = $(Get-File -Folder $workingFolder -FileName $labeledExamplesFileName -SkipExistanceCheck $true)
        [string]$labeledExamplesContent = $(Convert-LabeledExamplesToJson -LabeledExamples $labeledExamples)
        New-TextFile -File $labeledExamplesFile -Content $labeledExamplesContent

        [Logger]::Log([string]::Format("The Labeled Examples File has been successfully created: '{0}'.", $labeledExamplesFile))

        [System.IO.FileInfo]$textSnippetsFile = $(Get-File -Folder $workingFolder -FileName $textSnippetsFileName -SkipExistanceCheck $true)
        [string]$textSnippetsContent = $(Convert-TextSnippetsToJson -TextSnippets $textSnippets)
        New-TextFile -File $textSnippetsFile -Content $textSnippetsContent

        [Logger]::Log([string]::Format("The Text Snippets File has been successfully created: '{0}'.", $textSnippetsFile))       

        [Logger]::Log("Dataset processing completed.")

    }
    catch {

        [Logger]::Log($_.Exception.Message)
        [Logger]::Log("Dataset processing aborted.")
    
    }        

}

# Main
Clear-Host
Invoke-Script