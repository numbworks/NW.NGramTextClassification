using System;
using System.Reflection;
using NW.NGramTextClassification.Filenames;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.Shared.Files;
using NW.Shared.Serialization;
using NW.Shared.Validation;

namespace NW.NGramTextClassification.Bags
{
    /// <summary>Collects all the dependencies required by <see cref="TextClassifier"/>.</summary>
    public class ComponentBag
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
        public static string DefaultLoggingActionDateFormat { get; } = "yyyy-MM-dd HH:mm:ss:fff";
        public static Action<string> DefaultLoggingAction { get; }
            = (message) => Console.WriteLine($"[{DateTime.UtcNow.ToString(DefaultLoggingActionDateFormat)}] {message}");
        public static Action<string> DefaultLoggingActionAsciiBanner { get; }
            = (message) => Console.WriteLine($"{message}");
        public static Func<DateTime> DefaultNowFunction { get; } = () => DateTime.Now;
        public static Func<string> DefaultVersionFunction { get; }
            = () =>
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Version version = assembly.GetName().Version;
               
                if (version is not null)
                    return $"{version.Major}.{version.Minor}.{version.Build}";

                return "0.0.0";
                
            };

        public INGramTokenizer NGramsTokenizer { get; }
        public ISimilarityIndexCalculator SimilarityIndexCalculator { get; }
        public Func<double, double> RoundingFunction { get; }
        public Func<string, uint, string> TextTruncatingFunction { get; }
        public Action<string> LoggingAction { get; }
        public ILabeledExampleManager LabeledExampleManager { get; }
        public Action<string> LoggingActionAsciiBanner { get; }
        public IFileManager FileManager { get; }
        public ISerializerFactory SerializerFactory { get; }
        public IFilenameFactory FilenameFactory { get; }
        public Func<DateTime> NowFunction { get; }
        public Func<string> VersionFunction { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ComponentBag"/> instance.</summary>
        public ComponentBag(
                    INGramTokenizer nGramsTokenizer,
                    ISimilarityIndexCalculator similarityIndexCalculator,
                    Func<double, double> roundingFunction,
                    Func<string, uint, string> textTruncatingFunction,
                    Action<string> loggingAction,
                    ILabeledExampleManager labeledExampleManager,
                    Action<string> loggingActionAsciiBanner,
                    IFileManager fileManager,
                    ISerializerFactory serializerFactory,
                    IFilenameFactory filenameFactory,
                    Func<DateTime> nowFunction,
                    Func<string> versionFunction)
        {

            Validator.ValidateObject(nGramsTokenizer, nameof(nGramsTokenizer));
            Validator.ValidateObject(similarityIndexCalculator, nameof(similarityIndexCalculator));
            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));
            Validator.ValidateObject(textTruncatingFunction, nameof(textTruncatingFunction));
            Validator.ValidateObject(loggingAction, nameof(loggingAction));
            Validator.ValidateObject(labeledExampleManager, nameof(labeledExampleManager));
            Validator.ValidateObject(loggingActionAsciiBanner, nameof(loggingActionAsciiBanner));
            Validator.ValidateObject(fileManager, nameof(fileManager));
            Validator.ValidateObject(serializerFactory, nameof(serializerFactory));
            Validator.ValidateObject(filenameFactory, nameof(filenameFactory));
            Validator.ValidateObject(nowFunction, nameof(nowFunction));
            Validator.ValidateObject(versionFunction, nameof(versionFunction));

            NGramsTokenizer = nGramsTokenizer;
            SimilarityIndexCalculator = similarityIndexCalculator;
            RoundingFunction = roundingFunction;
            TextTruncatingFunction = textTruncatingFunction;
            LoggingAction = loggingAction;
            LabeledExampleManager = labeledExampleManager;
            LoggingActionAsciiBanner = loggingActionAsciiBanner;
            FileManager = fileManager;
            SerializerFactory = serializerFactory;
            FilenameFactory = filenameFactory;
            NowFunction = nowFunction;
            VersionFunction = versionFunction;

        }

        /// <summary>Initializes a <see cref="ComponentBag"/> instance using default parameters.</summary>
        public ComponentBag()
            : this(
                  nGramsTokenizer: new NGramTokenizer(),
                  similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                  roundingFunction: DefaultRoundingFunction,
                  textTruncatingFunction: DefaultTextTruncatingFunction,
                  loggingAction: DefaultLoggingAction,
                  labeledExampleManager: new LabeledExampleManager(),
                  loggingActionAsciiBanner: DefaultLoggingActionAsciiBanner,
                  fileManager: new FileManager(),
                  serializerFactory: new SerializerFactory(),
                  filenameFactory: new FilenameFactory(),
                  nowFunction: DefaultNowFunction,
                  versionFunction: DefaultVersionFunction)
        { }

        #endregion

        #region Methods_public
        #endregion

    }
}