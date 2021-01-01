using System;
using System.Collections.Generic;
using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient
{
    class Program
    {
        static void Main(string[] args)
        {

            string text = "Vår kund erbjuder trivsel";
            List<LabeledExtract> labeledExtracts = new List<LabeledExtract>()
            {

                new LabeledExtractFactory().Create(1, "sv", "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö."),
                new LabeledExtractFactory().Create(2, "en", "We are looking for several skilled and driven developers to join our team in Lund.")

            };

            ITextClassifier textClassifier = new TextClassifier();
            TextClassifierResult result = textClassifier.Predict(text, labeledExtracts);

            Console.WriteLine(result.Label);


            Console.ReadKey();
        }

    }
}
