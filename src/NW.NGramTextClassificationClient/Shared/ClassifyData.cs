using System;
using System.Collections;
using System.Collections.Generic;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>Collects all the data required by the <c>classify</c> sub-command.</summary>
    public class ClassifyData
    {

        #region Fields

        #endregion

        #region Properties

        public string FolderPath { get; }
        public string LabeledExamples { get; }
        public string TextSnippets { get; }
        public string TokenizerRuleset { get; }
        public double? MinAccuracySingle { get; }
        public double? MinAccuracyMultiple { get; }
        public bool SaveSession { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ClassifyData"/> instance using default parameters.</summary>	
        public ClassifyData(
                string folderPath,
                string labeledExamples,
                string textSnippets,
                string tokenizerRuleset,
                double? minAccuracySingle,
                double? minAccuracyMultiple,
                bool? saveSession
            ) 
        {

            FolderPath = folderPath;
            LabeledExamples = labeledExamples;
            TextSnippets = textSnippets;
            TokenizerRuleset = tokenizerRuleset;
            MinAccuracySingle = minAccuracySingle;
            MinAccuracyMultiple = minAccuracyMultiple;

            if (saveSession == null)
                SaveSession = false;
            else
                SaveSession = (bool)saveSession;

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
    Last Update: 22.10.2022
*/