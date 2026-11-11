using System;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.TextClassifiers;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <inheritdoc cref="ITextClassificationManagerFactory/>
    public class TextClassificationManagerFactory : ITextClassificationManagerFactory
    {

        #region Fields
        #endregion

        #region Properties

        public static Func<string> DefaultVersionFunction { get; } = TextClassifier.DefaultVersionFunction;
        public static Func<string, string> DefaultErrorFormattingFunction { get; } = ComponentBag.DefaultErrorFormattingFunction;

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassificationManagerFactory"/> instance.</summary>
        public TextClassificationManagerFactory() { }

        #endregion

        #region Methods (public)

        public ITextClassificationManager Create(SettingBag settingBag, out Action<string> loggingAction)
        {

            loggingAction = ComponentBag.DefaultLoggingAction;
            Func<DateTime> nowFunction = TextClassifier.DefaultNowFunction;

            if (settingBag.EnableUTC)
            {
                loggingAction = ComponentBag.DefaultUTCLoggingAction; 
                nowFunction = TextClassifier.DefaultUTCNowFunction;
            }

            ITextClassifier textClassifier = new TextClassifier(nowFunction: nowFunction, versionFunction : DefaultVersionFunction);
            ComponentBag componentBag = new ComponentBag(textClassifier: textClassifier, loggingAction: loggingAction, errorFormattingFunction: DefaultErrorFormattingFunction);
            TextClassificationManager textClassificationManager = new TextClassificationManager(settingBag: settingBag, componentBag: componentBag);

            return textClassificationManager;

        }

        #endregion

    }
}