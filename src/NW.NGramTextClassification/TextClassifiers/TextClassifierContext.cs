using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.TextClassifiers
{
    /// <summary>The context required to perform a text classification.</summary>
    public class TextClassifierContext
    {

        #region Fields
        #endregion

        #region Properties

        public INGramTokenizerRuleSet TokenizerRuleSet { get; }
        public List<LabeledExample> LabeledExamples { get; }
        public double MinimumAccuracySingleLabel { get; }
        public double MinimumAccuracyMultipleLabels { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifierContext"/> instance.</summary>
        public TextClassifierContext(
            INGramTokenizerRuleSet tokenizerRuleSet,
            List<LabeledExample> labeledExamples,
            double minimumAccuracySingleLabel,
            double minimumAccuracyMultipleLabels)
        {

            TokenizerRuleSet = tokenizerRuleSet;
            LabeledExamples = labeledExamples;
            MinimumAccuracySingleLabel = minimumAccuracySingleLabel;
            MinimumAccuracyMultipleLabels = minimumAccuracyMultipleLabels;

        }

        /// <summary>Initializes a <see cref="TextClassifierContext"/> instance with default tokenizer ruleset.</summary>
        public TextClassifierContext(
            List<LabeledExample> labeledExamples,
            double minimumAccuracySingleLabel,
            double minimumAccuracyMultipleLabels)
            : this(null, labeledExamples, minimumAccuracySingleLabel, minimumAccuracyMultipleLabels)
        {

        }

        #endregion

        #region Methods (public)
        #endregion

    }
}