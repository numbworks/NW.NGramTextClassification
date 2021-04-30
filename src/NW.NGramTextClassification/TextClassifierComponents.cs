using System;

namespace NW.NGramTextClassification
{
    public class TextClassifierComponents
    {

        // Fields
        // Properties (static)
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

        // Properties
        public INGramTokenizer NGramsTokenizer { get; private set; }
        public ISimilarityIndexCalculator SimilarityIndexCalculator { get; private set; }
        public Func<double, double> RoundingFunction { get; private set; }
        public Func<string, uint, string> TextTruncatingFunction { get; private set; }
        public Action<string> LoggingAction { get; private set; }

        // Constructors
        public TextClassifierComponents(
                INGramTokenizer nGramsTokenizer,
                ISimilarityIndexCalculator similarityIndexCalculator,
                Func<double, double> roundingFunction,
                Func<string, uint, string> textTruncatingFunction,
                Action<string> loggingAction)
        {

            Validator.ValidateObject(nGramsTokenizer, nameof(nGramsTokenizer));
            Validator.ValidateObject(similarityIndexCalculator, nameof(similarityIndexCalculator));
            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));
            Validator.ValidateObject(textTruncatingFunction, nameof(textTruncatingFunction));
            Validator.ValidateObject(loggingAction, nameof(loggingAction));

            NGramsTokenizer = nGramsTokenizer;
            SimilarityIndexCalculator = similarityIndexCalculator;
            RoundingFunction = roundingFunction;
            TextTruncatingFunction = textTruncatingFunction;
            LoggingAction = loggingAction;

        }
        public TextClassifierComponents()
            : this(
                  new NGramTokenizer(), 
                  new SimilarityIndexCalculatorJaccard(), 
                  DefaultRoundingFunction,
                  DefaultTextTruncatingFunction,
                  DefaultLoggingAction) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.04.2021

*/