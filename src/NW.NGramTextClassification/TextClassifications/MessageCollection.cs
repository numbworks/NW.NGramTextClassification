using System;
using System.Collections.Generic;
using System.Linq;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.TextClassifiers;
using NW.NGramTextClassification.TextSnippets;

namespace NW.NGramTextClassification.TextClassifications
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="TextClassifications"/>.</summary>
    public static class MessageCollection
    {

        #region TextClassificationManager

        public static Func<string, string> LoadingFileNameReturnedDefault =
            (fileName) => $"Loading the content of '{fileName}' returned a default value. Please check the content of the file, it may be null or invalid.";

        public static string TextClassificationSessionStarted = "The text classification session has been started.";
        public static string TextClassificationSessionCompleted = "The text classification session has been completed.";

        public static Func<string, string, string> TheProvidedIs = (name, value) => $"The provided '{name}' is: '{value}'.";
        
        public static Func<List<LabeledExample>, string> XLabeledExamplesHaveBeenLoaded =
            (labeledExamples) => $"'{labeledExamples.Count}' {nameof(LabeledExample)} objects have been loaded.";
        public static Func<List<TextSnippet>, string> XTextSnippetsHaveBeenLoaded =
            (textSnippets) => $"'{textSnippets.Count}' {nameof(TextSnippet)} objects have been loaded.";

        public static Func<List<LabeledExample>, string> LabeledExamplesHaveBeenThruCleaningProcess =
            (labeledExamples) =>$"The provided {nameof(LabeledExample)} objects have been thru the cleaning process. Now they are: '{labeledExamples.Count}'.";

        public static string TextClassificationSessionHasBeenSuccessful = "The text classification session has been successful.";

        public static Func<List<TextClassifierResult>, string> TextClassificationSessionFollowingResults =
            (results) => $"The text classification session has produced the following results: {RollOutCollection(results?.Cast<object>() ?? Enumerable.Empty<object>())}";

        public static Func<string, string> ClassifyOutputSuccessfullySaved =
            (folderPath) => $"The {nameof(ClassifyOutput)} object has been successfully saved in '{folderPath}'.";

        #endregion

        #region TextClassifier

        public static Func<Type, string> ThereIsNoStrategyOutOfType =
            (type) => $"There is no built-in strategy to create a filename out of a '{type.Name}' object.";

        #endregion

        #region Methods

        private static string RollOutCollection(IEnumerable<object> coll)
        {

            List<string> list = new List<string>();

            foreach (object obj in coll)
                list.Add(obj.ToString());

            return $"[{string.Join(", ", list)}]";

        }

        #endregion

    }
}