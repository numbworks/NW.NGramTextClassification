using System;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextClassifiers;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <inheritdoc cref="ITextClassificationManagerFactory/>
    public class TextClassificationManagerFactory : ITextClassificationManagerFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassificationManagerFactory"/> instance.</summary>
        public TextClassificationManagerFactory() { }

        #endregion

        #region Methods (public)

        public ITextClassificationManager Create(bool enableUTC = false)
        {

            Func<DateTime> nowFunction = TextClassifier.DefaultNowFunction;
            Action<string> loggingAction = ComponentBag.DefaultLoggingAction; 

            if (enableUTC)
            {
                nowFunction = TextClassifier.DefaultUTCNowFunction;
                loggingAction = ComponentBag.DefaultUTCLoggingAction; 
            }

            ITextClassifier textClassifier = new TextClassifier(nowFunction: nowFunction);
            ComponentBag componentBag = new ComponentBag(textClassifier: textClassifier, loggingAction: loggingAction);

            TextClassificationManager textClassificationManager = new TextClassificationManager(componentBag: componentBag);

            return textClassificationManager;

        }

        #endregion

    }
}