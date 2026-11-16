using System;
using NW.NGramTextClassification.TextClassifiers;
using NW.Shared.Files;

namespace NW.NGramTextClassification.Bags
{
    /// <summary>Collects all the dependencies required by <see cref="TextClassificationManager"/>.</summary>
    public class ComponentBag
    {

        #region Fields
        #endregion

        #region Properties

        public static string DefaultLoggingActionDateFormat { get; } = "yyyy-MM-dd HH:mm:ss:fff";
        public static Action<string> DefaultLoggingAction { get; }
            = (message) => Console.WriteLine($"[{TextClassifiers.TextClassifier.DefaultNowFunction().ToString(DefaultLoggingActionDateFormat)}] {message}");
        public static Action<string> DefaultUTCLoggingAction { get; }
            = (message) => Console.WriteLine($"[{TextClassifiers.TextClassifier.DefaultUTCNowFunction().ToString(DefaultLoggingActionDateFormat)}] {message}");
        public static Func<string, string> DefaultErrorFormattingFunction = (message) => $"ERROR: {message}";

        public IFileManager FileManager { get; }
        public ITextClassifier TextClassifier { get; }
        public Action<string> LoggingAction { get; }
        public Func<string, string> ErrorFormattingFunction { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes an instance of <see cref="ComponentBag"/>.</summary>
        public ComponentBag(
            IFileManager fileManager = null, 
            ITextClassifier textClassifier = null, 
            Action<string> loggingAction = null,
            Func<string, string> errorFormattingFunction = null)
        {

            FileManager = fileManager ?? new FileManager();
            TextClassifier = textClassifier ?? new TextClassifier();
            LoggingAction = loggingAction ?? DefaultLoggingAction;
            ErrorFormattingFunction = errorFormattingFunction ?? DefaultErrorFormattingFunction;

        }

        #endregion

        #region Methods (public)
        #endregion

    }
}