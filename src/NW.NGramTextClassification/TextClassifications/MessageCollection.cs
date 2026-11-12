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

        public static string TextClassificationProcessStarted = "The text classification process has been started.";
        public static string TextClassificationProcessCompleted = "The text classification process has been completed.";

        public static Func<string, string, string> TheProvidedIs = (name, value) => $"The provided '{name}' is: '{value}'.";
        
        public static Func<List<LabeledExample>, string> XLabeledExamplesHaveBeenLoaded =
            (labeledExamples) => $"'{labeledExamples.Count}' {nameof(LabeledExample)} objects have been loaded.";
        public static Func<List<TextSnippet>, string> XTextSnippetsHaveBeenLoaded =
            (textSnippets) => $"'{textSnippets.Count}' {nameof(TextSnippet)} objects have been loaded.";

        public static Func<List<LabeledExample>, string> LabeledExamplesHaveBeenThruCleaningProcess =
            (labeledExamples) =>$"The provided {nameof(LabeledExample)} objects have been thru the cleaning process. Now they are: '{labeledExamples.Count}'.";

        public static string TextClassificationOutputSuccessfullyCreated = "The text classification output has been successfully created.";

        public static string TextClassificationOutputContainsResults
            => $"The text classification output contains the following results:";

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