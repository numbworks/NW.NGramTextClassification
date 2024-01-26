using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NW.NGramTextClassification.Files;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Serializations;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification
{
    /// <inheritdoc cref="ITextClassifier"/>
    public class TextClassifier : ITextClassifier
    {

        #region Fields

        private ComponentCollection _componentCollection;
        private SettingCollection _settingCollection;

        #endregion

        #region Properties

        public static ComponentCollection DefaultComponentCollection { get; } = new ComponentCollection();
        public static SettingCollection DefaultSettingCollection { get; } = new SettingCollection();
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

        public string Version { get; }
        public string AsciiBanner { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifier"/> instance.</summary>
        public TextClassifier(ComponentCollection componentCollection, SettingCollection settingCollection)
        {

            Validator.ValidateObject(componentCollection, nameof(componentCollection));
            Validator.ValidateObject(settingCollection, nameof(settingCollection));

            _componentCollection = componentCollection;
            _settingCollection = settingCollection;

            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AsciiBanner = _componentCollection.AsciiBannerManager.Create(Version);

        }

        /// <summary>Initializes a <see cref="TextClassifier"/> instance using default parameters.</summary>
        public TextClassifier()
            : this(DefaultComponentCollection, DefaultSettingCollection) { }

        #endregion

        #region Methods_public

        public TextClassifierSession ClassifyOrDefault(TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            Validator.ValidateObject(textSnippet, nameof(textSnippet));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            LogInitialMessages(textSnippet, tokenizerRuleSet, labeledExamples);

            List<TokenizedExample> tokenizedExamples = TokenizeAndLog(tokenizerRuleSet, labeledExamples);
            
            if (tokenizedExamples == null)
            {

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.AtLeastOneLabeledExampleFailedTokenized);
               
                TextClassifierResult result = DefaultTextClassifierResult;
                TextClassifierSession session = CreateSession(_settingCollection, result, Version);

                return session;

            }
            else
            {

                TextClassifierResult result = ClassifySingleOrDefault(textSnippet, tokenizerRuleSet, tokenizedExamples);
                TextClassifierSession session = CreateSession(_settingCollection, result, Version);

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

            _componentCollection.LoggingAction(TextClassifications.MessageCollection.ProvidedSnippetsAre(textSnippets.Count));

            List<TokenizedExample> tokenizedExamples = TokenizeAndLog(tokenizerRuleSet, labeledExamples);

            if (tokenizedExamples == null)
            {

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.AtLeastOneLabeledExampleFailedTokenized);

                List<TextClassifierResult> results = new List<TextClassifierResult>();
                results.Add(DefaultTextClassifierResult);

                TextClassifierSession session = CreateSession(_settingCollection, results, Version);

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
                TextClassifierSession session = CreateSession(_settingCollection, finalResults, Version);

                return session;

            }

        }
        public TextClassifierSession ClassifyMany(List<TextSnippet> textSnippets, List<LabeledExample> labeledExamples)
                => ClassifyMany(textSnippets, DefaultNGramTokenizerRuleSet, labeledExamples);

        public void LogAsciiBanner()
            => _componentCollection.LoggingActionAsciiBanner(AsciiBanner);
        public IFileInfoAdapter Convert(string filePath)
            => _componentCollection.FileManager.Create(filePath);

        public List<LabeledExample> LoadLabeledExamplesOrDefault(IFileInfoAdapter jsonFile)
            => LoadManyOrDefault<LabeledExample>(jsonFile);
        public List<TextSnippet> LoadTextSnippetsOrDefault(IFileInfoAdapter jsonFile)
            => LoadManyOrDefault<TextSnippet>(jsonFile);
        public NGramTokenizerRuleSet LoadTokenizerRuleSetOrDefault(IFileInfoAdapter jsonFile)
            => LoadOrDefault<NGramTokenizerRuleSet>(jsonFile);

        public void SaveLabeledExamples(List<LabeledExample> labeledExamples, string folderPath)
            => Save(objects: labeledExamples, jsonFile: Create<LabeledExample>(folderPath: folderPath, now: _componentCollection.NowFunction()));
        public void SaveTextSnippets(List<TextSnippet> textSnippets, string folderPath)
            => Save(objects: textSnippets, jsonFile: Create<TextSnippet>(folderPath: folderPath, now: _componentCollection.NowFunction()));
        public void SaveSession(TextClassifierSession session, string folderPath, bool disableIndexSerialization)
        {

            if(disableIndexSerialization)
            {

                dynamic newSession = SimilarityIndexDisabler(session);
                Save(obj: newSession, jsonFile: Create<TextClassifierSession>(folderPath: folderPath, now: _componentCollection.NowFunction()));

            }
            else
                Save(obj: session, jsonFile: Create<TextClassifierSession>(folderPath: folderPath, now: _componentCollection.NowFunction()));

        }

        public List<LabeledExample> CleanLabeledExamples(List<LabeledExample> labeledExamples, INGramTokenizerRuleSet tokenizerRuleSet)
        {

            _componentCollection.LoggingAction(TextClassifications.MessageCollection.AttemptingToCleanLabeledExamples);

            List<LabeledExample> removed;
            List<LabeledExample> clean = _componentCollection.LabeledExampleManager.CleanLabeledExamples(labeledExamples, tokenizerRuleSet, out removed);

            _componentCollection.LoggingAction(TextClassifications.MessageCollection.ProvidedLabeledExamplesThruCleaningProcess);

            if (removed.Count > 0)
                foreach (LabeledExample labeledExample in removed)
                    _componentCollection.LoggingAction(TextClassifications.MessageCollection.ThisLabeledExampleHasBeenRemoved(labeledExample));
            else
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.NoLabeledExampleHasBeenRemoved);

            return clean;

        }

        #endregion

        #region Methods_private

        private void LogInitialMessages(TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            string truncated = _componentCollection.TextTruncatingFunction(textSnippet.Text, _settingCollection.TruncateTextInLogMessagesAfter);

            _componentCollection.LoggingAction(TextClassifications.MessageCollection.AttemptingToClassifyProvidedSnippet);
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingSnippetHasBeenProvided(truncated));
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingNGramsTokenizerRuleSetWillBeUsed(tokenizerRuleSet));
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.XLabeledExamplesHaveBeenProvided(labeledExamples));

        }
        private List<TokenizedExample> TokenizeAndLog(INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            List<TokenizedExample> tokenizedExamples = _componentCollection.LabeledExampleManager.CreateOrDefault(labeledExamples, tokenizerRuleSet);
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.ProvidedLabeledExamplesThruTokenizationProcess);

            return tokenizedExamples;

        }
        private TextClassifierResult ClassifySingleOrDefault(TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<TokenizedExample> tokenizedExamples)
        {

            List<INGram> nGrams = _componentCollection.NGramsTokenizer.DoForRuleSetOrDefault(textSnippet.Text, tokenizerRuleSet);
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.ProvidedTextHasBeenTokenizedIntoXNGrams(nGrams));
            if (nGrams == null)
            {

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.AllRulesInProvidedRulesetFailed(textSnippet.Text));
                return DefaultTextClassifierResult;

            }

            return CreateResult(textSnippet, nGrams, tokenizedExamples);

        }
        private TextClassifierResult CreateResult(TextSnippet textSnippet, List<INGram> nGrams, List<TokenizedExample> tokenizedExamples)
        {

            List<SimilarityIndex> indexes = GetSimilarityIndexes(nGrams, tokenizedExamples);
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.TokenizedSnippetComparedAgainstProvidedTokenizedExamples);
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.XSimilarityIndexObjectsHaveBeenComputed(indexes));

            List<SimilarityIndexAverage> indexAverages = GetSimilarityIndexAverages(indexes);
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.XSimilarityIndexAverageObjectsHaveBeenComputed(indexAverages));

            string label = GetLabel(indexAverages);
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.ResultOfClassificationTaskIs(label));

            if (label == null)
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ClassificationTaskHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples);
            else
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ClassificationTaskHasBeenSuccessful);

            TextClassifierResult result = new TextClassifierResult(textSnippet, label, indexes, indexAverages);

            return result;

        }        
        private TextClassifierSession CreateSession(SettingCollection settingCollection, TextClassifierResult result, string version)
        {

            List<TextClassifierResult> results = new List<TextClassifierResult>();
            results.Add(result);

            return new TextClassifierSession(settingCollection: settingCollection, results: results, version: version);

        }
        private TextClassifierSession CreateSession(SettingCollection settingCollection, List<TextClassifierResult> results, string version)
            => new TextClassifierSession(settingCollection: settingCollection, results: results, version: version);

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

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ComparingProvidedSnippetAgainstFollowingTokenizedExample(tokenizedExamples[i]));

                double indexValue = _componentCollection.SimilarityIndexCalculator.Do(nGrams, tokenizedExamples[i].NGrams, _componentCollection.RoundingFunction);
                double roundedValue = _componentCollection.RoundingFunction(indexValue);

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.CalculatedSimilarityIndexValueIs(indexValue));
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.RoundedSimilarityIndexValueIs(roundedValue));

                SimilarityIndex similarityIndex = new SimilarityIndex(tokenizedExamples[i].LabeledExample.Text, tokenizedExamples[i].LabeledExample.Label, roundedValue);
                similarityIndexes.Add(similarityIndex);

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingSimilarityIndexObjectHasBeenAddedToTheList(similarityIndex));

            }

            return similarityIndexes;

        }
        private List<SimilarityIndexAverage> GetSimilarityIndexAverages(List<SimilarityIndex> indexes)
        {

            List<string> uniqueLabels = GetUniqueLabels(indexes);
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingUniqueLabelsHaveBeenFound(uniqueLabels));

            List<SimilarityIndexAverage> similarityAverages = new List<SimilarityIndexAverage>();
            for (int i = 0; i < uniqueLabels.Count; i++)
            {

                string currentLabel = uniqueLabels[i];
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.CalculatingIndexAverageForTheFollowingLabel(currentLabel));

                List<double> indexValues = GetSimilarityIndexValues(currentLabel, indexes);
                double averageValue = CalculateAverage(indexValues);
                double roundedValue = _componentCollection.RoundingFunction(averageValue);

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.CalculatedSimilarityIndexAverageValueIs(averageValue));
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.RoundedSimilarityIndexAverageValueIs(roundedValue));

                SimilarityIndexAverage indexAverage = new SimilarityIndexAverage(currentLabel, roundedValue);
                similarityAverages.Add(indexAverage);

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingSimilarityIndexAverageObjectHasBeenAddedToTheList(indexAverage));

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
            {
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(AreAllIndexAveragesEqualToZero)));
                return null;
            }
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(AreAllIndexAveragesEqualToZero)));

            if (IsSingleLabelAndHigherEqualThanMinimumAccuracy(indexAverages, _settingCollection))
            {
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(IsSingleLabelAndHigherEqualThanMinimumAccuracy)));           
                return LogAndReturnLabel(indexAverages);
            }
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(IsSingleLabelAndHigherEqualThanMinimumAccuracy)));

            if (IsSingleLabelAndLessThanMinimumAccuracy(indexAverages, _settingCollection))
            {
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(IsSingleLabelAndLessThanMinimumAccuracy)));
                return null;
            }
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(IsSingleLabelAndLessThanMinimumAccuracy)));

            if (AreAllIndexAveragesSameValue(indexAverages))
            {
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(AreAllIndexAveragesSameValue)));
                return null;
            }
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(AreAllIndexAveragesSameValue)));

            indexAverages = OrderByHighest(indexAverages);
            
            if (AreTwoHighestIndexAveragesSameValue(indexAverages))
            {
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(AreTwoHighestIndexAveragesSameValue)));
                return null;
            }
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(AreTwoHighestIndexAveragesSameValue)));

            if (IsLessThanMinimumAccuracyMultipleLabels(indexAverages, _settingCollection))
            {
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(IsLessThanMinimumAccuracyMultipleLabels)));
                return null;
            }
            _componentCollection.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(IsLessThanMinimumAccuracyMultipleLabels)));

            return LogAndReturnLabel(indexAverages);

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
        private bool IsSingleLabelAndHigherEqualThanMinimumAccuracy(List<SimilarityIndexAverage> indexAverages, SettingCollection settingCollection)
            => (indexAverages.Count == 1 && indexAverages[0].Value >= settingCollection.MinimumAccuracySingleLabel);
        private bool IsSingleLabelAndLessThanMinimumAccuracy(List<SimilarityIndexAverage> indexAverages, SettingCollection settingCollection)
            => (indexAverages.Count == 1 && indexAverages[0].Value < settingCollection.MinimumAccuracySingleLabel);
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

            return (indexAverages[0].Value == indexAverages[1].Value);

        }
        private bool IsLessThanMinimumAccuracyMultipleLabels(List<SimilarityIndexAverage> indexAverages, SettingCollection settingCollection)
            => (indexAverages[0].Value < settingCollection.MinimumAccuracyMultipleLabels);
        private string LogAndReturnLabel(List<SimilarityIndexAverage> indexAverages)
        {

            _componentCollection.LoggingAction(TextClassifications.MessageCollection.SimilarityIndexAverageWithTheHighestValueIs(indexAverages[0]));

            return indexAverages[0].Label;

        }
        private List<SimilarityIndexAverage> OrderByHighest(List<SimilarityIndexAverage> indexAverages)
            => indexAverages.OrderByDescending(Item => Item.Value).ToList();

        private List<T> LoadManyOrDefault<T>(IFileInfoAdapter jsonFile)
        {

            Validator.ValidateObject(jsonFile, nameof(jsonFile));
            Validator.ValidateFileExistance(jsonFile);

            _componentCollection.LoggingAction(TextClassifications.MessageCollection.AttemptingToLoadObjectsFrom(typeof(T), jsonFile));

            string content = _componentCollection.FileManager.ReadAllText(jsonFile);

            ISerializer<T> serializer = _componentCollection.SerializerFactory.Create<T>();
            List<T> objects = serializer.DeserializeManyOrDefault(content);

            if (objects == Serializer<T>.Default)
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ObjectsFailedToLoad(typeof(T)));
            else
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ObjectsSuccessfullyLoaded(typeof(T)));

            return objects;

        }
        private T LoadOrDefault<T>(IFileInfoAdapter jsonFile)
        {

            Validator.ValidateObject(jsonFile, nameof(jsonFile));
            Validator.ValidateFileExistance(jsonFile);

            _componentCollection.LoggingAction(TextClassifications.MessageCollection.AttemptingToLoadObjectFrom(typeof(T), jsonFile));

            string content = _componentCollection.FileManager.ReadAllText(jsonFile);

            ISerializer<T> serializer = _componentCollection.SerializerFactory.Create<T>();
            T obj = serializer.DeserializeOrDefault(content);

            if (EqualityComparer<T>.Default.Equals(obj, default(T)))
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ObjectFailedToLoad(typeof(T)));
            else
                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ObjectSuccessfullyLoaded(typeof(T)));

            return obj;

        }
        private void Save<T>(List<T> objects, IFileInfoAdapter jsonFile)
        {

            _componentCollection.LoggingAction(TextClassifications.MessageCollection.AttemptingToSaveObjectsAs(typeof(T), jsonFile));

            try
            {

                ISerializer<T> serializer = _componentCollection.SerializerFactory.Create<T>();
                string content = serializer.Serialize(objects);

                _componentCollection.FileManager.WriteAllText(jsonFile, content);

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ObjectsSuccessfullySaved(typeof(T)));

            }
            catch
            {

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ObjectsFailedToSave(typeof(T)));

            }

        }
        private void Save<T>(T obj, IFileInfoAdapter jsonFile)
        {

            _componentCollection.LoggingAction(TextClassifications.MessageCollection.AttemptingToSaveObjectAs(typeof(T), jsonFile));

            try
            {

                ISerializer<T> serializer = _componentCollection.SerializerFactory.Create<T>();              
                string content = serializer.Serialize(obj);

                _componentCollection.FileManager.WriteAllText(jsonFile, content);

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ObjectSuccessfullySaved(typeof(T)));

            }
            catch
            {

                _componentCollection.LoggingAction(TextClassifications.MessageCollection.ObjectFailedToSave(typeof(T)));

            }

        }
        private IFileInfoAdapter Create<T>(string folderPath, DateTime now)
        {

            string filePath;

            if (typeof(T) == typeof(LabeledExample))
                filePath = _componentCollection.FilenameFactory.CreateForLabeledExamplesJson(folderPath: folderPath, now: now);
            else if (typeof(T) == typeof(TextSnippet))
                filePath = _componentCollection.FilenameFactory.CreateForTextSnippetsJson(folderPath: folderPath, now: now);
            else if (typeof(T) == typeof(TextClassifierSession))
                filePath = _componentCollection.FilenameFactory.CreateForSessionJson(folderPath: folderPath, now: now);
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

/*
    Author: numbworks@gmail.com
    Last Update: 26.12.2022
*/