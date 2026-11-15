using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NW.NGramTextClassification.Filenames;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;
using NW.Shared.Files;
using NW.Shared.Files.Validation;
using NW.Shared.Serialization;
using NW.Shared.Validation;

namespace NW.NGramTextClassification.TextClassifiers
{
    /// <inheritdoc cref="ITextClassifier"/>
    public class TextClassifier : ITextClassifier
    {

        #region Fields

        private IFileManager _fileManager;
        private ILabeledExampleManager _labeledExampleManager;
        private INGramTokenizer _nGramsTokenizer;
        private ISerializerFactory _serializerFactory;
        private IFilenameFactory _filenameFactory;
        private ISimilarityIndexCalculator _similarityIndexCalculator;
        private Func<DateTime> _nowFunction;
        private Func<string> _versionFunction;
        private Func<double, double> _roundingFunction;

        #endregion

        #region Properties

        public static INGramTokenizerRuleSet DefaultNGramTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();
        public static TextClassifierResult DefaultTextClassifierResult { get; } 
            = new TextClassifierResult(null, null, new List<SimilarityIndex>(), new List<SimilarityIndexAverage>());
        public static Func<ClassifyOutput, dynamic> SimilarityIndexDisabler
            = (classifyOutput) =>
            {

                List<dynamic> newResults = new List<dynamic>();
                foreach(TextClassifierResult result in classifyOutput.Results)
                {

                    dynamic newResult = new ExpandoObject();

                    newResult.TextSnippet = result.TextSnippet;
                    newResult.Label = result.Label;
                    newResult.SimilarityIndexes = new List<SimilarityIndex>();
                    newResult.SimilarityIndexAverages = result.SimilarityIndexAverages;

                    newResults.Add(newResult);

                }

                dynamic newSession = new ExpandoObject();

                newSession.MinimumAccuracySingleLabel = classifyOutput.MinimumAccuracySingleLabel;
                newSession.MinimumAccuracyMultipleLabels = classifyOutput.MinimumAccuracyMultipleLabels;
                newSession.Results = newResults;
                newSession.Version = classifyOutput.Version;

                return newSession;
            };
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
        public static Func<double, double> DefaultRoundingFunction { get; }
            = x => Math.Round(x, 6, MidpointRounding.AwayFromZero);

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifier"/> instance.</summary>
        public TextClassifier(
            IFileManager fileManager = null,
            ILabeledExampleManager labeledExampleManager = null,
            INGramTokenizer nGramsTokenizer = null,
            ISerializerFactory serializerFactory = null,
            IFilenameFactory filenameFactory = null,
            ISimilarityIndexCalculator similarityIndexCalculator = null,
            Func<DateTime> nowFunction = null,
            Func<string> versionFunction = null,
            Func<double, double> roundingFunction = null
            )
        {

            _fileManager = fileManager ?? new FileManager();
            _labeledExampleManager = labeledExampleManager ?? new LabeledExampleManager();
            _nGramsTokenizer = nGramsTokenizer ?? new NGramTokenizer();
            _serializerFactory = serializerFactory ?? new SerializerFactory();
            _filenameFactory = filenameFactory ?? new FilenameFactory();
            _similarityIndexCalculator = similarityIndexCalculator ?? new SimilarityIndexCalculatorJaccard();
            _nowFunction = nowFunction ?? DefaultNowFunction;
            _versionFunction = versionFunction ?? DefaultVersionFunction;
            _roundingFunction = roundingFunction ?? DefaultRoundingFunction;

        }

        #endregion

        #region Methods (public)

        public ClassifyOutput ClassifyOrDefault(
            TextSnippet textSnippet, 
            INGramTokenizerRuleSet tokenizerRuleSet, 
            List<LabeledExample> labeledExamples,
            double minimumAccuracySingleLabel, 
            double minimumAccuracyMultipleLabels)
        {

            Validator.ValidateObject(textSnippet, nameof(textSnippet));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            List<TokenizedExample> tokenizedExamples = TokenizeOrDefault(tokenizerRuleSet, labeledExamples);
            string version = _versionFunction();

            if (tokenizedExamples == null)
            {

                TextClassifierResult result = DefaultTextClassifierResult;
                ClassifyOutput classifyOutput = new ClassifyOutput(minimumAccuracySingleLabel, minimumAccuracyMultipleLabels, [result], version);

                return classifyOutput;

            }
            else
            {

                TextClassifierResult result = ClassifySingleOrDefault(textSnippet, tokenizerRuleSet, tokenizedExamples, minimumAccuracySingleLabel, minimumAccuracyMultipleLabels);
                ClassifyOutput classifyOutput = new ClassifyOutput(minimumAccuracySingleLabel, minimumAccuracyMultipleLabels, [result], version);

                return classifyOutput;

            }               

        }
        
        public ClassifyOutput ClassifyOrDefault(
            TextSnippet textSnippet, 
            List<LabeledExample> labeledExamples, 
            double minimumAccuracySingleLabel, 
            double minimumAccuracyMultipleLabels)
                => ClassifyOrDefault(textSnippet, DefaultNGramTokenizerRuleSet, labeledExamples, minimumAccuracySingleLabel, minimumAccuracyMultipleLabels);

        public ClassifyOutput ClassifyManyOrDefault(
            List<TextSnippet> textSnippets, 
            INGramTokenizerRuleSet tokenizerRuleSet, 
            List<LabeledExample> labeledExamples,
            double minimumAccuracySingleLabel, 
            double minimumAccuracyMultipleLabels) 
        {

            Validator.ValidateList(textSnippets, nameof(textSnippets));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            List<TokenizedExample> tokenizedExamples = TokenizeOrDefault(tokenizerRuleSet, labeledExamples);
            string version = _versionFunction();

            if (tokenizedExamples == null)
            {

                List<TextClassifierResult> results = [DefaultTextClassifierResult];
                ClassifyOutput classifyOutput = new ClassifyOutput(minimumAccuracySingleLabel, minimumAccuracyMultipleLabels, results, version);

                return classifyOutput;

            }
            else
            {

                ConcurrentBag<TextClassifierResult> tempResults = new ConcurrentBag<TextClassifierResult>();
                Parallel.For(0, textSnippets.Count, i =>
                {

                    TextClassifierResult result = ClassifySingleOrDefault(textSnippets[i], tokenizerRuleSet, tokenizedExamples, minimumAccuracySingleLabel, minimumAccuracyMultipleLabels);
                    tempResults.Add(result);

                });

                List<TextClassifierResult> finalResults = RestoreOrderOrDefault(tempResults, textSnippets);
                ClassifyOutput classifyOutput = new ClassifyOutput(minimumAccuracySingleLabel, minimumAccuracyMultipleLabels, finalResults, version);

                return classifyOutput;

            }

        }
        
        public ClassifyOutput ClassifyManyOrDefault(
            List<TextSnippet> textSnippets, 
            List<LabeledExample> labeledExamples,
            double minimumAccuracySingleLabel, 
            double minimumAccuracyMultipleLabels)
                => ClassifyManyOrDefault(textSnippets, DefaultNGramTokenizerRuleSet, labeledExamples, minimumAccuracySingleLabel, minimumAccuracyMultipleLabels);

        public IFileInfoAdapter Convert(string filePath)
            => _fileManager.Create(filePath);
        public List<LabeledExample> LoadLabeledExamplesOrDefault(IFileInfoAdapter jsonFile)
            => LoadManyOrDefault<LabeledExample>(jsonFile);
        public List<TextSnippet> LoadTextSnippetsOrDefault(IFileInfoAdapter jsonFile)
            => LoadManyOrDefault<TextSnippet>(jsonFile);
        public NGramTokenizerRuleSet LoadTokenizerRuleSetOrDefault(IFileInfoAdapter jsonFile)
            => LoadOrDefault<NGramTokenizerRuleSet>(jsonFile);
        public void SaveLabeledExamples(List<LabeledExample> labeledExamples, string folderPath)
            => Save(objects: labeledExamples, jsonFile: Create<LabeledExample>(folderPath: folderPath, now: _nowFunction()));
        public void SaveTextSnippets(List<TextSnippet> textSnippets, string folderPath)
            => Save(objects: textSnippets, jsonFile: Create<TextSnippet>(folderPath: folderPath, now: _nowFunction()));
        public void SaveClassifyOutput(ClassifyOutput classifyOutput, string folderPath, bool disableIndexSerialization)
        {

            if(disableIndexSerialization)
            {

                dynamic updatedOutput = SimilarityIndexDisabler(classifyOutput);
                Save(obj: updatedOutput, jsonFile: Create<ClassifyOutput>(folderPath: folderPath, now: _nowFunction()));

            }
            else
                Save(obj: classifyOutput, jsonFile: Create<ClassifyOutput>(folderPath: folderPath, now: _nowFunction()));

        }
        public List<LabeledExample> CleanLabeledExamples(List<LabeledExample> labeledExamples, INGramTokenizerRuleSet tokenizerRuleSet)
        {

            List<LabeledExample> clean = _labeledExampleManager.CleanLabeledExamples(labeledExamples, tokenizerRuleSet, out _);

            return clean;
        }

        #endregion

        #region Methods (private)
      
        private TextClassifierResult ClassifySingleOrDefault(
            TextSnippet textSnippet, 
            INGramTokenizerRuleSet tokenizerRuleSet, 
            List<TokenizedExample> tokenizedExamples,
            double minimumAccuracySingleLabel, 
            double minimumAccuracyMultipleLabels)
        {

            List<INGram> nGrams = _nGramsTokenizer.DoForRuleSetOrDefault(textSnippet.Text, tokenizerRuleSet);

            if (nGrams == null)
                return DefaultTextClassifierResult;

            return CreateResult(textSnippet, nGrams, tokenizedExamples, minimumAccuracySingleLabel, minimumAccuracyMultipleLabels);

        }

        private List<TokenizedExample> Tokenize(INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            List<TokenizedExample> tokenizedExamples = _labeledExampleManager.Create(labeledExamples, tokenizerRuleSet);

            return tokenizedExamples;

        }
        private List<TokenizedExample> TokenizeOrDefault(INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            List<TokenizedExample> tokenizedExamples = _labeledExampleManager.CreateOrDefault(labeledExamples, tokenizerRuleSet);

            return tokenizedExamples;

        }
        
        private TextClassifierResult CreateResult(TextSnippet textSnippet, List<INGram> nGrams, List<TokenizedExample> tokenizedExamples, double minimumAccuracySingleLabel, double minimumAccuracyMultipleLabels)
        {

            List<SimilarityIndex> indexes = GetSimilarityIndexes(nGrams, tokenizedExamples);
            List<SimilarityIndexAverage> indexAverages = GetSimilarityIndexAverages(indexes);

            string label = GetLabel(indexAverages, minimumAccuracySingleLabel, minimumAccuracyMultipleLabels);

            TextClassifierResult result = new TextClassifierResult(textSnippet, label, indexes, indexAverages);

            return result;

        }        
        private List<SimilarityIndex> GetSimilarityIndexes(List<INGram> nGrams, List<TokenizedExample> tokenizedExamples)
        {

            /*
                Returns something like: 
                    
                    Id Label Value
                    1  sv    0.01995
                    2  en    0.014888
                    3  en    0.002268
                    ...
             */

            List<SimilarityIndex> similarityIndexes = new List<SimilarityIndex>();
            for (int i = 0; i < tokenizedExamples.Count; i++)
            {

                double indexValue = _similarityIndexCalculator.Do(nGrams, tokenizedExamples[i].NGrams, _roundingFunction);
                double roundedValue = _roundingFunction(indexValue);

                SimilarityIndex similarityIndex = new SimilarityIndex(tokenizedExamples[i].LabeledExample.Text, tokenizedExamples[i].LabeledExample.Label, roundedValue);
                similarityIndexes.Add(similarityIndex);

            }

            return similarityIndexes;

        }
        private List<SimilarityIndexAverage> GetSimilarityIndexAverages(List<SimilarityIndex> indexes)
        {

            List<string> uniqueLabels = GetUniqueLabels(indexes);
            List<SimilarityIndexAverage> similarityAverages = new List<SimilarityIndexAverage>();

            for (int i = 0; i < uniqueLabels.Count; i++)
            {

                string currentLabel = uniqueLabels[i];

                List<double> indexValues = GetSimilarityIndexValues(currentLabel, indexes);
                double averageValue = CalculateAverage(indexValues);
                double roundedValue = _roundingFunction(averageValue);

                SimilarityIndexAverage indexAverage = new SimilarityIndexAverage(currentLabel, roundedValue);
                similarityAverages.Add(indexAverage);

            }

            return similarityAverages;

        }
        private List<string> GetUniqueLabels(List<SimilarityIndex> indexes)
        {

            /*
             * 
             * Label    Average
             * sv       0.19
             * en       0.45
             * en       0.11
             * en       0.98
             * 
             *      => { "sv", "en" }
             * 
             */

            List<string> labels = indexes.Select(similarityIndex => similarityIndex.Label).ToList();
            List<string> uniqueLabels = new HashSet<string>(labels).ToList();

            return uniqueLabels;

        }
        private List<double> GetSimilarityIndexValues(string label, List<SimilarityIndex> indexes)
        {

            /*
             * 
             * Label    SimilarityIndex
             * sv       0.19
             * en       0.45
             * en       0.12
             * 
             *      => en: { 0.45, 0.12 }
             * 
             */

            return indexes
                    .Where(similarityIndex => similarityIndex.Label == label)
                    .Select(similarityIndex => similarityIndex.Value)
                    .ToList();

        }
        private double CalculateAverage(List<double> averages)
        {

            /* { 0.19, 0.45 } => 0.32 */

            double sum = 0.0;
            foreach (double dbl in averages)
                sum += dbl;

            return sum / averages.Count;

        }

        private string GetLabel(List<SimilarityIndexAverage> indexAverages, double minimumAccuracySingleLabel, double minimumAccuracyMultipleLabels)
        {

            /*
             * 
             * Label    Average
             * sv       0.19
             * en       0.45
             * dk       0.27 
             * 
             *      => { Label: "en", Average: 0.45 } => "en" (main use case scenario)
             * 
             */

            if (AreAllIndexAveragesEqualToZero(indexAverages))
                return null;

            if (IsSingleLabelAndHigherEqualThanMinimumAccuracy(indexAverages, minimumAccuracySingleLabel))
                return ReturnLabel(indexAverages);
           
            if (IsSingleLabelAndLessThanMinimumAccuracy(indexAverages, minimumAccuracySingleLabel))
                return null;

            if (AreAllIndexAveragesSameValue(indexAverages))
                return null;
            
            indexAverages = OrderByHighest(indexAverages);
            
            if (AreTwoHighestIndexAveragesSameValue(indexAverages))
                return null;

            if (IsLessThanMinimumAccuracyMultipleLabels(indexAverages, minimumAccuracyMultipleLabels))
                return null;
            
            return ReturnLabel(indexAverages);

        }
        private bool AreAllIndexAveragesEqualToZero(List<SimilarityIndexAverage> indexAverages)
        {

            /*
             *
             * Label    Average
             * sv       0
             * en       0
             * 
             * 		=> { 0, 0 } 
             * 		=> true
             * 
             */

            if (indexAverages.Where(Item => Item.Value == 0).Count() == indexAverages.Count) // All equal to zero
                return true;

            return false;

        }
        private bool IsSingleLabelAndHigherEqualThanMinimumAccuracy(List<SimilarityIndexAverage> indexAverages, double minimumAccuracySingleLabel)
            => indexAverages.Count == 1 && indexAverages[0].Value >= minimumAccuracySingleLabel;
        private bool IsSingleLabelAndLessThanMinimumAccuracy(List<SimilarityIndexAverage> indexAverages, double minimumAccuracySingleLabel)
            => indexAverages.Count == 1 && indexAverages[0].Value < minimumAccuracySingleLabel;
        private bool AreAllIndexAveragesSameValue(List<SimilarityIndexAverage> indexAverages)
        {

            /*
             *
             * Label    Average
             * sv       0.1
             * en       0.1
             * dk       0.1
             * 
             * 		=> { 0.1, 0.1, 0.1 } 
             * 		=> 1 
             * 		=> 1 == 1
             * 		=> true
             * 
             */

            if (indexAverages.Select(Item => Item.Value).Distinct().Count() == 1) // All with the same value
                return true;

            return false;

        }
        private bool AreTwoHighestIndexAveragesSameValue(List<SimilarityIndexAverage> indexAverages)
        {

            /*
             *
             * Label    Average
             * sv       0.98        < 1st
             * en       0.98        < 2nd
             * dk       0.32        < 3rd
             * 
             * 		=> 0.98 == 0.98
             * 		=> true
             * 
             */

            return indexAverages[0].Value == indexAverages[1].Value;

        }
        private bool IsLessThanMinimumAccuracyMultipleLabels(List<SimilarityIndexAverage> indexAverages, double minimumAccuracyMultipleLabels)
            => indexAverages[0].Value < minimumAccuracyMultipleLabels;
        private string ReturnLabel(List<SimilarityIndexAverage> indexAverages)
            => indexAverages[0].Label;
        private List<SimilarityIndexAverage> OrderByHighest(List<SimilarityIndexAverage> indexAverages)
            => indexAverages.OrderByDescending(Item => Item.Value).ToList();

        private List<T> LoadManyOrDefault<T>(IFileInfoAdapter jsonFile)
        {

            Validator.ValidateObject(jsonFile, nameof(jsonFile));
            FilesValidator.ValidateFileExistance(jsonFile);

            string content = _fileManager.ReadAllText(jsonFile);

            ISerializer<T> serializer = _serializerFactory.Create<T>();
            List<T> objects = serializer.DeserializeManyOrDefault(content);

            return objects;

        }
        private T LoadOrDefault<T>(IFileInfoAdapter jsonFile)
        {

            Validator.ValidateObject(jsonFile, nameof(jsonFile));
            FilesValidator.ValidateFileExistance(jsonFile);

            string content = _fileManager.ReadAllText(jsonFile);

            ISerializer<T> serializer = _serializerFactory.Create<T>();
            T obj = serializer.DeserializeOrDefault(content);

            return obj;

        }
        private void Save<T>(List<T> objects, IFileInfoAdapter jsonFile)
        {

            ISerializer<T> serializer = _serializerFactory.Create<T>();
            string content = serializer.Serialize(objects);

            _fileManager.WriteAllText(jsonFile, content);

        }
        private void Save<T>(T obj, IFileInfoAdapter jsonFile)
        {

            ISerializer<T> serializer = _serializerFactory.Create<T>();              
            string content = serializer.Serialize(obj);

            _fileManager.WriteAllText(jsonFile, content);

        }
        private IFileInfoAdapter Create<T>(string folderPath, DateTime now)
        {

            string filePath;

            if (typeof(T) == typeof(LabeledExample))
                filePath = _filenameFactory.CreateForLabeledExamplesJson(folderPath: folderPath, now: now);
            else if (typeof(T) == typeof(TextSnippet))
                filePath = _filenameFactory.CreateForTextSnippetsJson(folderPath: folderPath, now: now);
            else if (typeof(T) == typeof(ClassifyOutput))
                filePath = _filenameFactory.CreateForSessionJson(folderPath: folderPath, now: now);
            else
                throw new Exception(TextClassifications.MessageCollection.ThereIsNoStrategyOutOfType(typeof(T)));

            IFileInfoAdapter jsonFile = new FileInfoAdapter(fileName: filePath);

            return jsonFile;

        }

        private List<TextClassifierResult> RestoreOrderOrDefault(ConcurrentBag<TextClassifierResult> tempResults, List<TextSnippet> textSnippets)
        {

            // Thread-safe collections don't guarantee the source order, therefore defaultResults are not in the same order as textSnippets.
            List<TextClassifierResult> defaultResults = tempResults.ToList();

            try
            {

                // If textSnippets contains duplicates, we can't perform the ConcurrentBag re-ordering using TextSnippet.Text as unique identifier.
                HashSet<TextSnippet> uniqueTextSnippets = new HashSet<TextSnippet>(textSnippets);
                if (textSnippets.Count > uniqueTextSnippets.Count)
                    return defaultResults;

                List<string> ids = textSnippets.Select(textSnippet => textSnippet.Text).ToList();

                // In some cases, a TextClassifierResult.TextSnippet is null, but it corresponds to TextSnippet.Text = null.
                List<TextClassifierResult> finalResults = defaultResults.OrderBy(result => ids.IndexOf(result.TextSnippet?.Text ?? null)).ToList();

                return finalResults;

            }
            catch
            {

                // If some other unpredictable error happens, we don't make the application fail but just return the default unordered result.
                return defaultResults;

            }

        }

        #endregion

    }
}