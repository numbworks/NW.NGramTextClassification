namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>Collects all the data required by the <c>classify</c> sub-command.</summary>
    public class ClassifyData
    {

        #region Fields

        #endregion

        #region Properties

        public string LabeledExamples { get; }
        public string TextSnippets { get; }
        public string FolderPath { get; }
        public string TokenizerRuleSet { get; }
        public double? MinAccuracySingle { get; }
        public double? MinAccuracyMultiple { get; }
        public bool SaveSession { get; }
        public bool CleanLabeledExamples { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ClassifyData"/> instance using default parameters.</summary>	
        public ClassifyData(
                string labeledExamples,
                string textSnippets,
                string folderPath,
                string tokenizerRuleSet,
                double? minAccuracySingle,
                double? minAccuracyMultiple,
                bool saveSession,
                bool cleanLabeledExamples
            ) 
        {

            LabeledExamples = labeledExamples;
            TextSnippets = textSnippets;

            FolderPath = folderPath;
            TokenizerRuleSet = tokenizerRuleSet;
            MinAccuracySingle = minAccuracySingle;
            MinAccuracyMultiple = minAccuracyMultiple;
            SaveSession = saveSession;
            CleanLabeledExamples = cleanLabeledExamples;

        }

        #endregion

        #region Methods_public

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 04.11.2022
*/