using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using NW.NGramTextClassification.Bags;
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

namespace NW.NGramTextClassification
{
    /// <inheritdoc cref="ITextClassifier"/>
    public class TextClassifier : ITextClassifier
    {

        #region Fields

        private ComponentBag _componentBag;
        private SettingBag _settingBag;

        #endregion

        #region Properties

        public static ComponentBag DefaultComponentBag { get; } = new ComponentBag();
        public static SettingBag DefaultSettingBag { get; } = new SettingBag();
        public static INGramTokenizerRuleSet DefaultNGramTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();
        public static TextClassifierResult DefaultTextClassifierResult { get; } 
            = new TextClassifierResult(null, null, new List<SimilarityIndex>(), new List<SimilarityIndexAverage>());
        public static Func<TextClassifierSession, dynamic> SimilarityIndexDisabler
            = (textClassifierSession) =>
            {

                List<dynamic> newResults = new List<dynamic>();
                foreach(TextClassifierResult result in textClassifierSession.Results)
                {

                    dynamic newResult = new ExpandoObject();

                    newResult.TextSnippet = result.TextSnippet;
                    newResult.Label = result.Label;
                    newResult.SimilarityIndexes = new List<SimilarityIndex>();
                    newResult.SimilarityIndexAverages = result.SimilarityIndexAverages;

                    newResults.Add(newResult);

                }

                dynamic newSession = new ExpandoObject();

                newSession.MinimumAccuracySingleLabel = textClassifierSession.MinimumAccuracySingleLabel;
                newSession.MinimumAccuracyMultipleLabels = textClassifierSession.MinimumAccuracyMultipleLabels;
                newSession.Results = newResults;
                newSession.Version = textClassifierSession.Version;

                return newSession;
            };

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifier"/> instance.</summary>
        public TextClassifier(ComponentBag componentBag, SettingBag settingBag)
        {

            Validator.ValidateObject(componentBag, nameof(componentBag));
            Validator.ValidateObject(settingBag, nameof(settingBag));

            _componentBag = componentBag;
            _settingBag = settingBag;

        }

        /// <summary>Initializes a <see cref="TextClassifier"/> instance using default parameters.</summary>
        public TextClassifier()
            : this(DefaultComponentBag, DefaultSettingBag) { }

        #endregion

        #region Methods (public)

        public TextClassifierSession ClassifyOrDefault(TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            Validator.ValidateObject(textSnippet, nameof(textSnippet));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            List<TokenizedExample> tokenizedExamples = TokenizeAndLog(tokenizerRuleSet, labeledExamples);
            string version = _componentBag.VersionFunction();

            if (tokenizedExamples == null)
            {

                TextClassifierResult result = DefaultTextClassifierResult;
                TextClassifierSession session = CreateSession(_settingBag, result, version);

                return session;

            }
            else
            {

                TextClassifierResult result = ClassifySingleOrDefault(textSnippet, tokenizerRuleSet, tokenizedExamples);
                TextClassifierSession session = CreateSession(_settingBag, result, version);

                return session;

            }               

        }
        public TextClassifierSession ClassifyOrDefault(TextSnippet textSnippet, List<LabeledExample> labeledExamples)
                => ClassifyOrDefault(textSnippet, DefaultNGramTokenizerRuleSet, labeledExamples);

        public TextClassifierSession ClassifyMany(List<TextSnippet> textSnippets, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples) 
        {

            Validator.ValidateList(textSnippets, nameof(textSnippets));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            List<TokenizedExample> tokenizedExamples = TokenizeAndLog(tokenizerRuleSet, labeledExamples);
            string version = _componentBag.VersionFunction();

            if (tokenizedExamples == null)
            {

                List<TextClassifierResult> results = [DefaultTextClassifierResult];
                TextClassifierSession session = CreateSession(_settingBag, results, version);

                return session;

            }
            else
            {

                ConcurrentBag<TextClassifierResult> tempResults = new ConcurrentBag<TextClassifierResult>();
                Parallel.For(0, textSnippets.Count, i =>
                {

                    TextClassifierResult result = ClassifySingleOrDefault(textSnippets[i], tokenizerRuleSet, tokenizedExamples);
                    tempResults.Add(result);

                });

                List<TextClassifierResult> finalResults = RestoreOrderOrDefault(tempResults, textSnippets);
                TextClassifierSession session = CreateSession(_settingBag, finalResults, version);

                return session;

            }

        }
        public TextClassifierSession ClassifyMany(List<TextSnippet> textSnippets, List<LabeledExample> labeledExamples)
                => ClassifyMany(textSnippets, DefaultNGramTokenizerRuleSet, labeledExamples);

        public IFileInfoAdapter Convert(string filePath)
            => _componentBag.FileManager.Create(filePath);

        public List<LabeledExample> LoadLabeledExamplesOrDefault(IFileInfoAdapter jsonFile)
            => LoadManyOrDefault<LabeledExample>(jsonFile);
        public List<TextSnippet> LoadTextSnippetsOrDefault(IFileInfoAdapter jsonFile)
            => LoadManyOrDefault<TextSnippet>(jsonFile);
        public NGramTokenizerRuleSet LoadTokenizerRuleSetOrDefault(IFileInfoAdapter jsonFile)
            => LoadOrDefault<NGramTokenizerRuleSet>(jsonFile);

        public void SaveLabeledExamples(List<LabeledExample> labeledExamples, string folderPath)
            => Save(objects: labeledExamples, jsonFile: Create<LabeledExample>(folderPath: folderPath, now: _componentBag.NowFunction()));
        public void SaveTextSnippets(List<TextSnippet> textSnippets, string folderPath)
            => Save(objects: textSnippets, jsonFile: Create<TextSnippet>(folderPath: folderPath, now: _componentBag.NowFunction()));
        public void SaveSession(TextClassifierSession session, string folderPath, bool disableIndexSerialization)
        {

            if(disableIndexSerialization)
            {

                dynamic newSession = SimilarityIndexDisabler(session);
                Save(obj: newSession, jsonFile: Create<TextClassifierSession>(folderPath: folderPath, now: _componentBag.NowFunction()));

            }
            else
                Save(obj: session, jsonFile: Create<TextClassifierSession>(folderPath: folderPath, now: _componentBag.NowFunction()));

        }

        public List<LabeledExample> CleanLabeledExamples(List<LabeledExample> labeledExamples, INGramTokenizerRuleSet tokenizerRuleSet)
        {

            List<LabeledExample> clean = _componentBag.LabeledExampleManager.CleanLabeledExamples(labeledExamples, tokenizerRuleSet, out _);

            return clean;
        }

        #endregion

        #region Methods (private)


        private List<TokenizedExample> TokenizeAndLog(INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            List<TokenizedExample> tokenizedExamples = _componentBag.LabeledExampleManager.CreateOrDefault(labeledExamples, tokenizerRuleSet);
            _componentBag.LoggingAction(TextClassifications.MessageCollection.ProvidedLabeledExamplesThruTokenizationProcess);

            return tokenizedExamples;

        }
        private TextClassifierResult ClassifySingleOrDefault(TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<TokenizedExample> tokenizedExamples)
        {

            List<INGram> nGrams = _componentBag.NGramsTokenizer.DoForRuleSetOrDefault(textSnippet.Text, tokenizerRuleSet);

            if (nGrams == null)
                return DefaultTextClassifierResult;

            return CreateResult(textSnippet, nGrams, tokenizedExamples);

        }
        private TextClassifierResult CreateResult(TextSnippet textSnippet, List<INGram> nGrams, List<TokenizedExample> tokenizedExamples)
        {

            List<SimilarityIndex> indexes = GetSimilarityIndexes(nGrams, tokenizedExamples);
            List<SimilarityIndexAverage> indexAverages = GetSimilarityIndexAverages(indexes);

            string label = GetLabel(indexAverages);

            TextClassifierResult result = new TextClassifierResult(textSnippet, label, indexes, indexAverages);

            return result;

        }        
        private TextClassifierSession CreateSession(SettingBag settingBag, TextClassifierResult result, string version)
        {

            List<TextClassifierResult> results = [result];
            TextClassifierSession session = new TextClassifierSession(settingBag: settingBag, results: results, version: version);

            return session;

        }
        private TextClassifierSession CreateSession(SettingBag settingBag, List<TextClassifierResult> results, string version)
            => new TextClassifierSession(settingBag: settingBag, results: results, version: version);

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

                double indexValue = _componentBag.SimilarityIndexCalculator.Do(nGrams, tokenizedExamples[i].NGrams, _componentBag.RoundingFunction);
                double roundedValue = _componentBag.RoundingFunction(indexValue);

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
                double roundedValue = _componentBag.RoundingFunction(averageValue);

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

        private string GetLabel(List<SimilarityIndexAverage> indexAverages)
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

            if (IsSingleLabelAndHigherEqualThanMinimumAccuracy(indexAverages, _settingBag))
                return ReturnLabel(indexAverages);
           
            if (IsSingleLabelAndLessThanMinimumAccuracy(indexAverages, _settingBag))
                return null;

            if (AreAllIndexAveragesSameValue(indexAverages))
                return null;
            
            indexAverages = OrderByHighest(indexAverages);
            
            if (AreTwoHighestIndexAveragesSameValue(indexAverages))
                return null;

            if (IsLessThanMinimumAccuracyMultipleLabels(indexAverages, _settingBag))
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
        private bool IsSingleLabelAndHigherEqualThanMinimumAccuracy(List<SimilarityIndexAverage> indexAverages, SettingBag settingBag)
            => indexAverages.Count == 1 && indexAverages[0].Value >= settingBag.MinimumAccuracySingleLabel;
        private bool IsSingleLabelAndLessThanMinimumAccuracy(List<SimilarityIndexAverage> indexAverages, SettingBag settingBag)
            => indexAverages.Count == 1 && indexAverages[0].Value < settingBag.MinimumAccuracySingleLabel;
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
        private bool IsLessThanMinimumAccuracyMultipleLabels(List<SimilarityIndexAverage> indexAverages, SettingBag settingBag)
            => indexAverages[0].Value < settingBag.MinimumAccuracyMultipleLabels;
        private string ReturnLabel(List<SimilarityIndexAverage> indexAverages)
            => indexAverages[0].Label;
        private List<SimilarityIndexAverage> OrderByHighest(List<SimilarityIndexAverage> indexAverages)
            => indexAverages.OrderByDescending(Item => Item.Value).ToList();

        private List<T> LoadManyOrDefault<T>(IFileInfoAdapter jsonFile)
        {

            Validator.ValidateObject(jsonFile, nameof(jsonFile));
            FilesValidator.ValidateFileExistance(jsonFile);

            string content = _componentBag.FileManager.ReadAllText(jsonFile);

            ISerializer<T> serializer = _componentBag.SerializerFactory.Create<T>();
            List<T> objects = serializer.DeserializeManyOrDefault(content);

            return objects;

        }
        private T LoadOrDefault<T>(IFileInfoAdapter jsonFile)
        {

            Validator.ValidateObject(jsonFile, nameof(jsonFile));
            FilesValidator.ValidateFileExistance(jsonFile);

            string content = _componentBag.FileManager.ReadAllText(jsonFile);

            ISerializer<T> serializer = _componentBag.SerializerFactory.Create<T>();
            T obj = serializer.DeserializeOrDefault(content);

            return obj;

        }
        private void Save<T>(List<T> objects, IFileInfoAdapter jsonFile)
        {

            ISerializer<T> serializer = _componentBag.SerializerFactory.Create<T>();
            string content = serializer.Serialize(objects);

            _componentBag.FileManager.WriteAllText(jsonFile, content);

        }
        private void Save<T>(T obj, IFileInfoAdapter jsonFile)
        {

            ISerializer<T> serializer = _componentBag.SerializerFactory.Create<T>();              
            string content = serializer.Serialize(obj);

            _componentBag.FileManager.WriteAllText(jsonFile, content);

        }
        private IFileInfoAdapter Create<T>(string folderPath, DateTime now)
        {

            string filePath;

            if (typeof(T) == typeof(LabeledExample))
                filePath = _componentBag.FilenameFactory.CreateForLabeledExamplesJson(folderPath: folderPath, now: now);
            else if (typeof(T) == typeof(TextSnippet))
                filePath = _componentBag.FilenameFactory.CreateForTextSnippetsJson(folderPath: folderPath, now: now);
            else if (typeof(T) == typeof(TextClassifierSession))
                filePath = _componentBag.FilenameFactory.CreateForSessionJson(folderPath: folderPath, now: now);
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