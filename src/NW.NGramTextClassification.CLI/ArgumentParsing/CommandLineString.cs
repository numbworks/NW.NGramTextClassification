using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    public static class CommandLineString
    {
        
        #region Application
        
        public static string APPLICATION_NAME { get; } = "nwngram";
        public static string APPLICATION_DESCRIPTION { get; } = "Command-line application to perform text classification tasks.";
        
        #endregion

        #region Session
        
        public static string COMMAND_SESSION_NAME { get; } = "session";
        public static string COMMAND_SESSION_DESCR { get; } 
            = "Groups all the features related to a single text classification session.";
        
        #endregion
        
        #region Session Classify
        
        public static string SUBCOMMAND_CLASSIFY_NAME { get; } = "classify";
        public static string SUBCOMMAND_CLASSIFY_DESCR { get; } 
            = "Attempt to classify the provided text snippet(s) according to the provided labeled examples.";

        public static string OPTION_LABELEDEXAMPLES_TEMPL { get; } = "--labeledexamples";
        public static string OPTION_LABELEDEXAMPLES_DESCR { get; } 
            = string.Concat(
                "The filename of the JSON file containing the labeled examples required to train the classifier. ",
                "The file needs to be stored in the working folder.");
        public static string OPTION_LABELEDEXAMPLES_ERRORMESSAGE { get; } 
            = $"{OPTION_LABELEDEXAMPLES_TEMPL} is required.";

        public static string OPTION_TEXTSNIPPETS_TEMPL { get; } = "--textsnippets";
        public static string OPTION_TEXTSNIPPETS_DESCR { get; } 
            = string.Concat(
                "The filename of the JSON file containing the text snippets that the user wants to classify. ",
                "The file needs to be stored in the working folder.");
        public static string OPTION_TEXTSNIPPETS_ERRORMESSAGE { get; } 
            = $"{OPTION_TEXTSNIPPETS_TEMPL} is required.";

        public static string OPTION_TOKENIZERRULESET_TEMPL { get; } = "--tokenizerruleset";
        public static string OPTION_TOKENIZERRULESET_DESCR { get; } 
            = string.Concat(
                "(Optional) ",
                "The filename of the JSON file containing the tokenizer ruleset. ",
                "The file needs to be stored in the working folder. ",
                "If not specified, default rules will be used.");

        public static string OPTION_FOLDERPATH_TEMPL { get; } = "--folderpath";
        public static string OPTION_FOLDERPATH_DESCR { get; } 
            = string.Concat(
                "(Optional) ",
                "The path of the working folder. ",
                $"If not specified, '{SettingBag.DefaultFolderPath}' will be used.");

        public static string OPTION_MINACCURACYSINGLE_TEMPL { get; } = "--minaccuracysingle";
        public static string OPTION_MINACCURACYSINGLE_DESCR { get; }
            = string.Concat(
                "(Optional) ",
                "When a single label provided as example, the minimum index average required to return it as classification result. ",
                "Value can be between 0.0 and 1.0. ",
                $"If not specified, '{SettingBag.DefaultMinimumAccuracySingleLabel}' will be used.");

        public static string OPTION_MINACCURACYMULTIPLE_TEMPL { get; } = "--minaccuracymultiple";
        public static string OPTION_MINACCURACYMULTIPLE_DESCR { get; }
            = string.Concat(
                "(Optional) ",
                "When multiple labels provided as example, the minimum index average required to return the highest among them as classification result. ",
                "Value can be between 0.0 and 1.0. ",
                $"If not specified, '{SettingBag.DefaultMinimumAccuracyMultipleLabels}' will be used.");

        public static string OPTION_SAVESESSION_TEMPL { get; } = "--savesession";
        public static string OPTION_SAVESESSION_DESCR { get; }
            = string.Concat(
                "(Optional) ",
                "If provided, the text classification session will be saved as JSON in the working folder.");

        public static string OPTION_CLEANLABELEDEXAMPLES_TEMPL { get; } = "--cleanlabeledexamples";
        public static string OPTION_CLEANLABELEDEXAMPLES_DESCR { get; }
            = string.Concat(
                "(Optional) ",
                "If provided, any labeled examples that cause the tokenizer to fail will be removed before starting the classification session.");

        public static string OPTION_DISABLEINDEXSERIALIZATION_TEMPL { get; } = "--disableindexserialization";
        public static string OPTION_DISABLEINDEXSERIALIZATION_DESCR { get; }
            = string.Concat(
                "(Optional) ",
                "To use in conjunction with '--savesession'. It has no effect if provided by its own.");
        
        #endregion

    }
}