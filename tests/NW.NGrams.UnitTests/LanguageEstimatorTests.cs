using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using RUBN.Shared;
using RUBN.AF;

namespace NW.NGrams.UnitTests
{
    [TestFixture]
    public class LanguageEstimatorTests
    {

        // Fields
        private static string _msgSuccessLabel = "The language for the provided text has been successfully estimated.";
        private static string _msgSuccessLists = "It hasn't been possible to estimate the language for the provided text, but similarity indexes and averages are provided.";
        private static string _errFailure = "It hasn't been possible to estimate the language nor to provide the similarity indexes and averages for the provided text.";
        private static string _strFakeFailure = "fakefailuremessage";
        private static string _strFakeSuccess = "fakesuccessmessage";
        private static List<string> _listNGrams = new List<string>() {
                            "vår kund erbjuder",
                            "kund erbjuder trivsel",
                            "erbjuder trivsel arbetsglädje",
                            "trivsel arbetsglädje och",
                            "arbetsglädje och en",
                            "och en trygg",
                            "en trygg arbetsmiljö",
                            "trygg arbetsmiljö",
                            "arbetsmiljö"
                        };
        private static List<LabeledTextSimilarityIndex> _listSimilarityIndexes 
            = new List<LabeledTextSimilarityIndex>() {
                    new LabeledTextSimilarityIndex(1, "sv", 0.62),
                    new LabeledTextSimilarityIndex(2, "en", 0.24),
                    new LabeledTextSimilarityIndex(3, "en", 0.12) };
        private static List<LabeledTextSimilarityAverage> _listSimilarityAverages
            = new List<LabeledTextSimilarityAverage>() {
                new LabeledTextSimilarityAverage("sv", 0.62),
                new LabeledTextSimilarityAverage("en", 0.18) };
        private static JobDetailsJson _objDetailsJson = new JobDetailsJson()
        {
            rubrik = "Coffee Capsules Manager",
            annonstext = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö."
        };
        private static JobDetailsJson _objDetailsJsonNoDescription = new JobDetailsJson()
        {
            rubrik = "Coffee Capsules Manager",
            annonstext = null
        };
        private static List<LabeledTextNGrams> _listLabeledTextsNGrams = new List<LabeledTextNGrams>() {
                    new LabeledTextNGrams(1, "sv", new List<string>() {
                        "vår kund erbjuder",
                        "kund erbjuder trivsel",
                        "erbjuder trivsel arbetsglädje",
                        "trivsel arbetsglädje och",
                        "arbetsglädje och en",
                        "och en trygg",
                        "en trygg arbetsmiljö",
                        "trygg arbetsmiljö",
                        "arbetsmiljö"
                    }),
                    new LabeledTextNGrams(2, "en", new List<string>() {
                        "we are looking",
                        "are looking for",
                        "looking for several",
                        "for several skilled",
                        "several skilled and",
                        "skilled and driven",
                        "and driven developers",
                        "driven developers to",
                        "developers to join",
                        "to join our",
                        "join our team",
                        "our team in",
                        "team in lund",
                        "in lund",
                        "lund"
                    }),
                    new LabeledTextNGrams(3, "en", new List<string>() {
                        "Are you interested",
                        "you interested in",
                        "interested in using",
                        "in using and",
                        "using and developing",
                        "and developing",
                        "developing"
                    })
        };
        private static TestCaseData[] _arrTestCases =
        {

            new TestCaseData(
                new Func<INGramsTextClassifier>(() =>
                        {
                            INGramsTextClassifier fakeClassifier = Substitute.For<INGramsTextClassifier>();
                            fakeClassifier.ConvertToNGrams(Arg.Any<string>(), Arg.Any<List<ITokenizationStrategy>>()).Returns(
                                  OutcomeBuilder.CreateFailure(_strFakeFailure).Get());
                            return fakeClassifier;
                        }),
                OutcomeBuilder.CreateFailure(_errFailure)
                              .Prepend(_strFakeFailure)
                              .Get()
            ).SetName(nameof(Do_ShouldReturnExpectedOutcome_WhenInvoked) + " {01}"),

            new TestCaseData(
                new Func<INGramsTextClassifier>(() =>
                        {
                            INGramsTextClassifier fakeClassifier = Substitute.For<INGramsTextClassifier>();
                            fakeClassifier.ConvertToNGrams(Arg.Any<string>(), Arg.Any<List<ITokenizationStrategy>>()).Returns(
                                  OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listNGrams).Get());
                            fakeClassifier.GetSimilarityIndexes(Arg.Any<List<string>>(), Arg.Any<List<LabeledTextNGrams>>()).Returns(
                                  OutcomeBuilder.CreateFailure(_strFakeFailure).Get());
                            return fakeClassifier;
                        }),
                OutcomeBuilder.CreateFailure(_errFailure)
                              .Prepend(_strFakeFailure)
                              .Get()
            ).SetName(nameof(Do_ShouldReturnExpectedOutcome_WhenInvoked) + " {02}"),

            new TestCaseData(
                new Func<INGramsTextClassifier>(() =>
                        {
                            INGramsTextClassifier fakeClassifier = Substitute.For<INGramsTextClassifier>();
                            fakeClassifier.ConvertToNGrams(Arg.Any<string>(), Arg.Any<List<ITokenizationStrategy>>()).Returns(
                                  OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listNGrams).Get());
                            fakeClassifier.GetSimilarityIndexes(Arg.Any<List<string>>(), Arg.Any<List<LabeledTextNGrams>>()).Returns(
                                  OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listSimilarityIndexes).Get());
                            fakeClassifier.GetSimilarityAverages(Arg.Any<List<LabeledTextSimilarityIndex>>()).Returns(
                                OutcomeBuilder.CreateFailure(_strFakeFailure).Get());
                            return fakeClassifier;
                        }),
                OutcomeBuilder.CreateFailure(_errFailure)
                              .Prepend(_strFakeFailure)
                              .Get()
            ).SetName(nameof(Do_ShouldReturnExpectedOutcome_WhenInvoked) + " {03}"),

            new TestCaseData(
                new Func<INGramsTextClassifier>(() =>
                        {
                            INGramsTextClassifier fakeClassifier = Substitute.For<INGramsTextClassifier>();
                            fakeClassifier.ConvertToNGrams(Arg.Any<string>(), Arg.Any<List<ITokenizationStrategy>>()).Returns(
                                  OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listNGrams).Get());
                            fakeClassifier.GetSimilarityIndexes(Arg.Any<List<string>>(), Arg.Any<List<LabeledTextNGrams>>()).Returns(
                                  OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listSimilarityIndexes).Get());
                            fakeClassifier.GetSimilarityAverages(Arg.Any<List<LabeledTextSimilarityIndex>>()).Returns(
                                OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listSimilarityAverages).Get());
                            fakeClassifier.EstimateLabel(Arg.Any<List<LabeledTextSimilarityAverage>>()).Returns(
                                OutcomeBuilder.CreateFailure(_strFakeFailure).Get());
                            return fakeClassifier;
                        }),
                OutcomeBuilder.CreateSuccess(_msgSuccessLists,
                        new LanguageEstimationResult(null, _listSimilarityIndexes, _listSimilarityAverages))
                            .Append(_strFakeFailure)
                            .Get()
            ).SetName(nameof(Do_ShouldReturnExpectedOutcome_WhenInvoked) + " {04}"),

            new TestCaseData(
                new Func<INGramsTextClassifier>(() =>
                        {
                            INGramsTextClassifier fakeClassifier = Substitute.For<INGramsTextClassifier>();
                            fakeClassifier.ConvertToNGrams(Arg.Any<string>(), Arg.Any<List<ITokenizationStrategy>>()).Returns(
                                  OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listNGrams).Get());
                            fakeClassifier.GetSimilarityIndexes(Arg.Any<List<string>>(), Arg.Any<List<LabeledTextNGrams>>()).Returns(
                                  OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listSimilarityIndexes).Get());
                            fakeClassifier.GetSimilarityAverages(Arg.Any<List<LabeledTextSimilarityIndex>>()).Returns(
                                OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listSimilarityAverages).Get());
                            fakeClassifier.EstimateLabel(Arg.Any<List<LabeledTextSimilarityAverage>>()).Returns(
                                OutcomeBuilder.CreateSuccess(_strFakeSuccess, "sv").Get());
                            return fakeClassifier;
                        }),
                OutcomeBuilder.CreateSuccess(_msgSuccessLabel,
                        new LanguageEstimationResult("sv", _listSimilarityIndexes, _listSimilarityAverages))
                            .Get()
            ).SetName(nameof(Do_ShouldReturnExpectedOutcome_WhenInvoked) + " {05}")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(_arrTestCases))]
        public void Do_ShouldReturnExpectedOutcome_WhenInvoked
            (Func<INGramsTextClassifier> fGetFakeClassifier, Outcome objExpected)
        {

            // Arrange
            LanguageEstimator objLanguageEstimator = new LanguageEstimator();
            objLanguageEstimator.NGramsTextClassifier = fGetFakeClassifier();

            // Act
            Outcome objActual = objLanguageEstimator.Do(
                new JobDetailsTextDecisionStrategy(_objDetailsJson), 
                _listLabeledTextsNGrams);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);

            if (objActual.Result != null)
                Assert.IsInstanceOf<LanguageEstimationResult>(objActual.Result); 
                // testing that all three items were the same would have been too much verbose.

        }

        [Test]
        public void Do_ShouldThrowAnException_WhenNGramsTextClassifierIsNull()
        {

            // Arrange
            LanguageEstimator objLanguageEstimator = new LanguageEstimator();
            objLanguageEstimator.NGramsTextClassifier = null;

            // Act
            Outcome objActual = objLanguageEstimator.Do(
                new JobDetailsTextDecisionStrategy(_objDetailsJson), 
                _listLabeledTextsNGrams);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }
        [Test]
        public void TextDecisionStrategy_ShouldReturnJobTitle_WhenJobDescriptionIsNull()
        {

            // Arrange
            LanguageEstimator objLanguageEstimator = new LanguageEstimator();
            string strExpected = new JobDetailsTextDecisionStrategy(_objDetailsJsonNoDescription).GetText();

            // Act
            // Assert
            Assert.AreEqual(strExpected, _objDetailsJsonNoDescription.rubrik);

        }

        // TearDown

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 23.08.2018

*/
