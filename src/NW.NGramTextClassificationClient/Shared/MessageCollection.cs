using NW.NGramTextClassification;
using NW.NGramTextClassificationClient.ApplicationSession;
using System;

namespace NW.NGramTextClassificationClient.Shared
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NGramTextClassificationClient"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static string Application_Name { get; } = "ngramtc";
        public static string Application_Description { get; } = "Command-line application to perform text classification tasks.";

        public static string About_Name { get; } = "about";
        public static string About_Description { get; } = "About this application.";
        public static string About_Information_Author = "Author: numbworks";
        public static string About_Information_Email = "Email: numbworks [AT] gmail [DOT] com";
        public static string About_Information_Url = @"Github: http://www.github.com/numbworks";
        public static string About_Information_License = "License: MIT License";

        public static string Session_Name { get; } = "session";
        public static string Session_Description { get; } 
            = "Groups all the features related to a single text classification session.";

        public static string Session_Classify_Name { get; } = "classify";
        public static string Session_Classify_Description { get; } 
            = "Attempt to classify the provided text snippet(s) according to the provided labeled examples.";

        public static string Session_Option_LabeledExamples_Template { get; } = "--labeledexamples";
        public static string Session_Option_LabeledExamples_Description { get; } 
            = $"The filename of the JSON file containing the labeled examples required to train the classifier. The file needs to be stored in the working folder.";
        public static string Session_Option_LabeledExamples_ErrorMessage { get; } 
            = $"{Session_Option_LabeledExamples_Template} is mandatory.";
        public static string Session_Option_TextSnippets_Template { get; } = "--textsnippets";
        public static string Session_Option_TextSnippets_Description { get; } 
            = $"The filename of the JSON file containing the text snippets that the user wants to classify. The file needs to be stored in the working folder.";
        public static string Session_Option_TextSnippets_ErrorMessage { get; } 
            = $"{Session_Option_TextSnippets_Template} is mandatory.";

        public static string Session_Option_TokenizerRuleSet_Template { get; } = "--tokenizerruleset";
        public static string Session_Option_TokenizerRuleSet_Description { get; } 
            = $"The filename of the JSON file containing the tokenizer ruleset. The file needs to be stored in the working folder. If not specified, default rules will be used.";

        public static string Session_Option_FolderPath_Template { get; } = "--folderpath";
        public static string Session_Option_FolderPath_Description { get; } 
            = $"The path of the working folder. If not specified, '{TextClassifierSettings.DefaultFolderPath}' will be used.";

        public static string Session_Option_MinAccuracySingle_Template { get; } = "--minaccuracysingle";
        public static string Session_Option_MinAccuracySingle_Description { get; }
            = string.Concat(
                "When a single label provided as example, the minimum index average required to return it as classification result. ",
                "Value can be between 0.0 and 1.0. ",
                $"If not specified, '{TextClassifierSettings.DefaultMinimumAccuracySingleLabel}' will be used."
            );
        public static string Session_Option_MinAccuracyMultiple_Template { get; } = "--minaccuracymultiple";
        public static string Session_Option_MinAccuracyMultiple_Description { get; }
            = string.Concat(
                "When multiple labels provided as example, the minimum index average required to return the highest among them as classification result. ",
                "Value can be between 0.0 and 1.0. ",
                $"If not specified, '{TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels}' will be used."
            );

        public static string Session_Option_SaveSession_Template { get; } = "--savesession";
        public static string Session_Option_SaveSession_Description { get; }
            = $"If provided, the text classification session will be saved as JSON in the working folder.";

        public static string Session_Option_CleanLabeledExamples_Template { get; } = "--cleanlabeledexamples";
        public static string Session_Option_CleanLabeledExamples_Description { get; }
            = $"If provided, the labeled examples among the provided ones that make the tokenizer to fail will be removed before starting the classification session.";

        public static Func<string, string> LoadingFileNameReturnedDefault =
            (fileName) => $"Loading the content of '{fileName}' returned a default value. Please check the content of the file, it may be null or invalid.";
        public static Func<string, string, string> ValueIsInvalidOrNotWithinRange
            = (name, value) => $"{name} ('{value}') is invalid or not within the expected range ('{nameof(DoubleManager.MininumValue)}':'{DoubleManager.MininumValue}', '{nameof(DoubleManager.MaximumValue)}':'{DoubleManager.MaximumValue}').";

        public static string PressAButtonToCloseTheWindow = "Press a button to close the window.";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 04.11.2022
*/