using System;

namespace NW.NGramTextClassification
{
    public class TextClassifierComponents
    {

        // Fields
        // Properties (static)
        public static Func<double, double> DefaultRoundingFunction { get; }
            = x => Math.Round(x, 6, MidpointRounding.AwayFromZero);
        public static Action<string> DefaultLoggingAction { get; }
            = (message) => Console.WriteLine(message);

        // Properties
        public INGramTokenizer NGramsTokenizer { get; private set; }
        public ISimilarityIndexCalculator SimilarityIndexCalculator { get; private set; }
        public Func<double, double> RoundingFunction { get; private set; }
        public Action<string> LoggingAction { get; private set; }

        // Constructors
        public TextClassifierComponents(
                INGramTokenizer nGramsTokenizer,
                ISimilarityIndexCalculator similarityIndexCalculator,
                Func<double, double> roundingFunction,
                Action<string> loggingAction)
        {

            Validator.ValidateObject(nGramsTokenizer, nameof(nGramsTokenizer));
            Validator.ValidateObject(similarityIndexCalculator, nameof(similarityIndexCalculator));
            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));
            Validator.ValidateObject(loggingAction, nameof(loggingAction));

            NGramsTokenizer = nGramsTokenizer;
            SimilarityIndexCalculator = similarityIndexCalculator;
            RoundingFunction = roundingFunction;
            LoggingAction = loggingAction;

        }
        public TextClassifierComponents()
            : this(
                  new NGramTokenizer(), 
                  new SimilarityIndexCalculatorJaccard(), 
                  DefaultRoundingFunction, 
                  DefaultLoggingAction) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 01.01.2021

*/