using System;
using NW.NGramTextClassification.AsciiBanner;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification
{
    /// <summary>Collects all the dependencies required by <see cref="TextClassifier"/>.</summary>
    public class TextClassifierComponents
    {

        #region Fields
        #endregion

        #region Properties

        public static Func<double, double> DefaultRoundingFunction { get; }
            = x => Math.Round(x, 6, MidpointRounding.AwayFromZero);
        public static Func<string, uint, string> DefaultTextTruncatingFunction { get; }
            = (text, length) =>
            {

                if (string.IsNullOrWhiteSpace(text))
                    return text;

                if (text.Length >= length)
                    return text.Substring(0, (int)length) + "...";

                return text;

            };
        public static Action<string> DefaultLoggingAction { get; }
            = (message) => Console.WriteLine(message);

        public INGramTokenizer NGramsTokenizer { get; }
        public ISimilarityIndexCalculator SimilarityIndexCalculator { get; }
        public Func<double, double> RoundingFunction { get; }
        public Func<string, uint, string> TextTruncatingFunction { get; }
        public Action<string> LoggingAction { get; }
        public ILabeledExampleManager LabeledExampleManager { get; }
        public IAsciiBannerManager AsciiBannerManager { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifierComponents"/> instance.</summary>
        public TextClassifierComponents(
                    INGramTokenizer nGramsTokenizer,
                    ISimilarityIndexCalculator similarityIndexCalculator,
                    Func<double, double> roundingFunction,
                    Func<string, uint, string> textTruncatingFunction,
                    Action<string> loggingAction,
                    ILabeledExampleManager labeledExampleManager,
                    IAsciiBannerManager asciiBannerManager)
        {

            Validator.ValidateObject(nGramsTokenizer, nameof(nGramsTokenizer));
            Validator.ValidateObject(similarityIndexCalculator, nameof(similarityIndexCalculator));
            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));
            Validator.ValidateObject(textTruncatingFunction, nameof(textTruncatingFunction));
            Validator.ValidateObject(loggingAction, nameof(loggingAction));
            Validator.ValidateObject(labeledExampleManager, nameof(labeledExampleManager));
            Validator.ValidateObject(asciiBannerManager, nameof(asciiBannerManager));

            NGramsTokenizer = nGramsTokenizer;
            SimilarityIndexCalculator = similarityIndexCalculator;
            RoundingFunction = roundingFunction;
            TextTruncatingFunction = textTruncatingFunction;
            LoggingAction = loggingAction;
            LabeledExampleManager = labeledExampleManager;
            AsciiBannerManager = asciiBannerManager;

        }

        /// <summary>Initializes a <see cref="TextClassifierComponents"/> instance using default parameters.</summary>
        public TextClassifierComponents()
            : this(
                  new NGramTokenizer(),
                  new SimilarityIndexCalculatorJaccard(),
                  DefaultRoundingFunction,
                  DefaultTextTruncatingFunction,
                  DefaultLoggingAction,
                  new LabeledExampleManager(),
                  new AsciiBannerManager())
        { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/