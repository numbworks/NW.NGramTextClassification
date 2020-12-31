using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.NGrams
{
    public class LabeledTextNGramsCreator : ILabeledTextNGramsCreator
    {

        // Fields
        private INGramsTextClassifier _NGramsTextClassifier;
        private ITokenizationStrategyManager _TokenizationStrategies;

        // Properties
        // Constructors
        public LabeledTextNGramsCreator(
            INGramsTextClassifier nGramsTextClassifier,
            ITokenizationStrategyManager tokenizationStrategies)
        {

            if (nGramsTextClassifier == null)
                throw new ArgumentNullException(nameof(nGramsTextClassifier));
            if (tokenizationStrategies == null)
                throw new ArgumentNullException(nameof(tokenizationStrategies));

            _NGramsTextClassifier = nGramsTextClassifier;
            _TokenizationStrategies = tokenizationStrategies;
        
        }
        public LabeledTextNGramsCreator()
            : this(
                new NGramsTextClassifier(), 
                new TokenizationStrategyManager()) { }

        // Methods
        public List<LabeledTextNGrams> Do(string labeledTextJson, string[] labels = null)
        {

            if (string.IsNullOrWhiteSpace(labeledTextJson))
                throw new ArgumentNullException(nameof(labeledTextJson));

            List <LabeledExtract> labeledTexts = _NGramsTextClassifier.GetLabeledTexts(labeledTextJson);
            if (labels != null)
                if (labels.Length > 0)
                    labeledTexts = labeledTexts.Where(obj => labels.Contains(obj.Label)).ToList();

            List<LabeledTextNGrams> labeledTextNGrams 
                = _NGramsTextClassifier.ConvertToNGrams(labeledTexts, _TokenizationStrategies.Get());

            return labeledTextNGrams;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/