using System;
using System.Collections.Generic;
using NW.NGramTextClassification;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.UnitTests;

namespace NW.NGramTextClassificationClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Run(() => RunExample1(), nameof(RunExample1));
            Run(() => RunExample2(), nameof(RunExample2));
            Run(() => RunExample3(), nameof(RunExample3));

            Console.ReadKey();

        }

        // Private methods
        private static void Run(Action action, string actionName)
        {

            Console.WriteLine(new string('=', 60));
            Console.WriteLine(actionName);
            Console.WriteLine(new string('=', 60));
            Console.WriteLine(Environment.NewLine);

            action.Invoke();

            Console.WriteLine(Environment.NewLine);

        }
        private static void RunExample1()
        {

            string text = "We are looking for several skilled and driven developers to join our team.";
            List<TokenizedExample> labeledExamples = ObjectMother.CreateLabeledExamples();

            ITextClassifier textClassifier = new TextClassifier();
            TextClassifierResult result = textClassifier.PredictLabel(text, labeledExamples);

            Console.WriteLine(result.Label);

        }
        private static void RunExample2()
        {

            string text = "Vår kund erbjuder trivsel";
            List<TokenizedExample> labeledExamples = ObjectMother.CreateLabeledExamples();

            ITextClassifier textClassifier = new TextClassifier();
            TextClassifierResult result = textClassifier.PredictLabelOrDefault(text, labeledExamples);

            Console.WriteLine(result.Label);

        }
        private static void RunExample3()
        {

            string text = "/";
            List<TokenizedExample> labeledExamples = ObjectMother.CreateLabeledExamples();

            ITextClassifier textClassifier = new TextClassifier();
            TextClassifierResult result = textClassifier.PredictLabelOrDefault(text, labeledExamples);

            Console.WriteLine(result.Label);

        }

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2021
*/