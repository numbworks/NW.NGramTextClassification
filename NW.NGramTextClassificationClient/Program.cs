using System;
using System.Collections.Generic;
using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient
{
    class Program
    {
        static void Main(string[] args)
        {

            RunExample1();

            Console.ReadKey();

        }

        static void RunExample1()
        {

            string text = "Vår kund erbjuder trivsel";
            List<LabeledExtract> labeledExtracts = new List<LabeledExtract>()
            {

                new LabeledExtractFactory().Create(1, "sv", "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö."),
                new LabeledExtractFactory().Create(2, "en", "We are looking for several skilled and driven developers to join our team in Lund.")

            };

            ITextClassifier textClassifier = new TextClassifier();
            TextClassifierResult result = textClassifier.PredictLabel(text, labeledExtracts);

            Console.WriteLine(result.Label);

        }
        static void RunExample2()
        {

            string text = "la fattoria degli animali fatati";
            List<LabeledExtract> labeledExtracts = new List<LabeledExtract>()
            {

                new LabeledExtractFactory().Create(1, "sv", "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö."),
                new LabeledExtractFactory().Create(2, "en", "We are looking for several skilled and driven developers to join our team in Lund.")

            };

            ITextClassifier textClassifier = new TextClassifier();
            TextClassifierResult result = textClassifier.PredictLabel(text, labeledExtracts);

            Console.WriteLine(result.Label);

        }


    }
}
