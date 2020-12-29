using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using RUBN.Shared;

namespace NW.NGrams.UnitTests
{
    [TestFixture]
    public class LabeledTextNGramsCreatorTests
    {

        // Fields
        private static string _strLabeledTextJson = String.Empty;
        private static string _msgSuccess = "A List<LabeledTextNGrams> has been created from the provided file path.";
        private static string _errFailure = "It hasn't been possible to create a List<LabeledTextNGrams> from the provided file path.";
        private static string _strFakeFailure = "fakefailuremessage";
        private static string _strFakeSuccess = "fakesuccessmessage";
        private static List<LabeledTextJson> _listLabeledTexts = new List<LabeledTextJson>() {
                    new LabeledTextJson()
                    {
                        LabeledTextId = 1,
                        Label = "sv",
                        Text = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö." },
                    new LabeledTextJson()
                    {
                        LabeledTextId = 2,
                        Label = "en",
                        Text = "We are looking for several skilled and driven developers to join our team in Lund." }
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
                    })
        };
        private static TestCaseData[] _arrTestCases =
        {

            new TestCaseData(
                new Func<INGramsTextClassifier>(() =>
                        {
                            INGramsTextClassifier fakeClassifier = Substitute.For<INGramsTextClassifier>();
                            fakeClassifier.GetLabeledTexts(Arg.Any<string>()).Returns(
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
                            fakeClassifier.GetLabeledTexts(Arg.Any<string>()).Returns(
                                  OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listLabeledTexts).Get());
                            fakeClassifier.ConvertToNGrams(Arg.Any<List<LabeledTextJson>>(), Arg.Any<List<ITokenizationStrategy>>()).Returns(
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
                            fakeClassifier.GetLabeledTexts(Arg.Any<string>()).Returns(
                                  OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listLabeledTexts).Get());
                            fakeClassifier.ConvertToNGrams(Arg.Any<List<LabeledTextJson>>(), Arg.Any<List<ITokenizationStrategy>>()).Returns(
                                  OutcomeBuilder.CreateSuccess(_strFakeSuccess, _listLabeledTextsNGrams).Get());
                            return fakeClassifier;
                        }),
                OutcomeBuilder.CreateSuccess(_msgSuccess, _listLabeledTextsNGrams)
                              .Get()
            ).SetName(nameof(Do_ShouldReturnExpectedOutcome_WhenInvoked) + " {03}")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(_arrTestCases))]
        public void Do_ShouldReturnExpectedOutcome_WhenInvoked
            (Func<INGramsTextClassifier> fGetFakeClassifier, Outcome objExpected)
        {

            // Arrange
            LabeledTextNGramsCreator objNGramsCreator = new LabeledTextNGramsCreator(_strLabeledTextJson);
            objNGramsCreator.NGramsTextClassifier = fGetFakeClassifier();

            // Act
            Outcome objActual = objNGramsCreator.Do();

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);

            if (objExpected.Result != null)
                for (int i = 0; i < ((List<LabeledTextNGrams>)objExpected.Result).Count; i++)
                {
                    Assert.AreEqual(((List<LabeledTextNGrams>)objExpected.Result)[i].LabeledTextId, ((List<LabeledTextNGrams>)objActual.Result)[i].LabeledTextId);
                    Assert.AreEqual(((List<LabeledTextNGrams>)objExpected.Result)[i].Label, ((List<LabeledTextNGrams>)objActual.Result)[i].Label);
                    Assert.AreEqual(((List<LabeledTextNGrams>)objExpected.Result)[i].NGrams, ((List<LabeledTextNGrams>)objActual.Result)[i].NGrams);
                }

        }

        [Test]
        public void Do_ShouldThrowAnException_WhenNGramsTextClassifierIsNull()
        {

            // Arrange
            LabeledTextNGramsCreator objNGramsCreator = new LabeledTextNGramsCreator(_strLabeledTextJson);
            objNGramsCreator.NGramsTextClassifier = null;

            // Act
            Outcome objActual = objNGramsCreator.Do();

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        // TearDown

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 17.02.2019

*/
