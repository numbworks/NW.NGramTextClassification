using System;
using System.Reflection;
using NW.NGramTextClassification.Filenames;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.Shared.Files;
using NW.Shared.Serialization;

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
            = (message) => Console.WriteLine($"[{DefaultNowFunction().ToString(DefaultLoggingActionDateFormat)}] {message}");
        public static Action<string> DefaultLoggingActionAsciiBanner { get; }
            = (message) => Console.WriteLine($"{message}");
        public static Func<DateTime> DefaultNowFunction { get; } = () => DateTime.Now;
        public static Func<DateTime> DefaultUTCNowFunction { get; }= () => DateTime.UtcNow;
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
                    INGramTokenizer nGramsTokenizer = null,
                    ISimilarityIndexCalculator similarityIndexCalculator = null,
                    Func<double, double> roundingFunction = null,
                    Func<string, uint, string> textTruncatingFunction = null,
                    Action<string> loggingAction = null,
                    ILabeledExampleManager labeledExampleManager = null,
                    Action<string> loggingActionAsciiBanner = null,
                    IFileManager fileManager = null,
                    ISerializerFactory serializerFactory = null,
                    IFilenameFactory filenameFactory = null,
                    Func<DateTime> nowFunction = null,
                    Func<string> versionFunction = null)
        {

            NGramsTokenizer = nGramsTokenizer ?? new NGramTokenizer();
            SimilarityIndexCalculator = similarityIndexCalculator ?? new SimilarityIndexCalculatorJaccard();
            RoundingFunction = roundingFunction ?? DefaultRoundingFunction;
            TextTruncatingFunction = textTruncatingFunction ?? DefaultTextTruncatingFunction;
            LoggingAction = loggingAction ?? DefaultLoggingAction;
            LabeledExampleManager = labeledExampleManager ?? new LabeledExampleManager();
            LoggingActionAsciiBanner = loggingActionAsciiBanner ?? DefaultLoggingActionAsciiBanner;
            FileManager = fileManager ?? new FileManager();
            SerializerFactory = serializerFactory ?? new SerializerFactory();
            FilenameFactory = filenameFactory ?? new FilenameFactory();
            NowFunction = nowFunction ?? DefaultNowFunction;
            VersionFunction = versionFunction ?? DefaultVersionFunction;

        }

        #endregion

        #region Methods (public)
        #endregion

    }
}