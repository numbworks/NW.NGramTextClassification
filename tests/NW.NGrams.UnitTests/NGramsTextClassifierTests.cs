using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using NSubstitute;
using RUBN.Shared;
using RUBN.UnitTestsUtilities;

namespace NW.NGrams.UnitTests
{
    [TestFixture]
    public class NGramsTextClassifierTests
    {

        // Fields
        private static string _strLabeledTextJson = Properties.Resources.LabeledTextsJson_Example;
        private static string _errParameterValidator = "The parameter at position '{0}' is null or empty.";
        private static ITokenizationStrategy _objTokenizationStrategy = new TokenizationStrategyTrigrams();
        private static TestCaseData[] _arrFormatAsTableList =
        {

            new TestCaseData(
                new List<ILabeledTextSimilarityValue>() {
                    new LabeledTextSimilarityIndex(3, "sv", 0.3),
                },
                OutcomeBuilder.CreateSuccess(
                    "The provided list of LabeledTextSimilarityValue objects has been successfully formatted.",
                    String.Concat("LabeledTextId\tLabel\tSimilarityIndex", Environment.NewLine, "3\tsv\t", 0.3.ToString())).Get()
            ).SetName(nameof(FormatAsTable_ShouldReturnTheExpectedOutcome_WhenListILabeledTextSimilarityValue) + " {01}"),

            new TestCaseData(
                new List<ILabeledTextSimilarityValue>() {
                    new LabeledTextSimilarityAverage("sv", 0.3),
                    new LabeledTextSimilarityAverage("en", 0.11)
                },
                OutcomeBuilder.CreateSuccess(
                   "The provided list of LabeledTextSimilarityValue objects has been successfully formatted.",
                    String.Concat("Label\tAverage", Environment.NewLine, "sv\t", 0.3.ToString(), Environment.NewLine, "en\t", 0.11.ToString())).Get()
            ).SetName(nameof(FormatAsTable_ShouldReturnTheExpectedOutcome_WhenListILabeledTextSimilarityValue) + " {02}"),

            new TestCaseData(
                new List<ILabeledTextSimilarityValue>(),
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        "The parameter at position '0' is null or empty.",
                        "It hasn't been possible to format the provided list of LabeledTextSimilarityValue objects."
                    }).Get()            
            ).SetName(nameof(FormatAsTable_ShouldReturnTheExpectedOutcome_WhenListILabeledTextSimilarityValue) + " {03}"),

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        "The parameter at position '0' is null or empty.",
                        "It hasn't been possible to format the provided list of LabeledTextSimilarityValue objects."
                    }).Get()
            ).SetName(nameof(FormatAsTable_ShouldReturnTheExpectedOutcome_WhenListILabeledTextSimilarityValue) + " {04}")

        };
        private static TestCaseData[] _arrFormatAsTable =
        {

            new TestCaseData(
                new LabeledTextSimilarityIndex(3, "sv", 0.3),
                OutcomeBuilder.CreateSuccess(
                    "The provided LabeledTextSimilarityValue object has been successfully formatted.",
                    String.Concat("LabeledTextId\tLabel\tSimilarityIndex", Environment.NewLine, "3\tsv\t", 0.3.ToString())).Get()
            ).SetName(nameof(FormatAsTable_ShouldReturnTheExpectedOutcome_WhenILabeledTextSimilarityValue) + " {01}"),

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        "The parameter at position '0' is null or empty.",
                        "It hasn't been possible to format the provided LabeledTextSimilarityValue object."
                    }).Get()
            ).SetName(nameof(FormatAsTable_ShouldReturnTheExpectedOutcome_WhenILabeledTextSimilarityValue) + " {02}")

        };
        private static TestCaseData[] _arrPrivateCalculateAverage =
        {

            new TestCaseData(
                new List<double>() { 0.19, 0.45 },
                OutcomeBuilder.CreateSuccess(
                    "The average among the provided values has been calculated.",
                    0.32).Get()
            ).SetName(nameof(PrivateCalculateAverage_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {01}"),

            new TestCaseData(
                new List<double>() { },
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to calculate the average among the provided values."
                    }).Get()
            ).SetName(nameof(PrivateCalculateAverage_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {02}"),

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to calculate the average among the provided values."
                    }).Get()
            ).SetName(nameof(PrivateCalculateAverage_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {03}")

        };
        private static TestCaseData[] _arrPrivateExtractUniqueLabels =
        {

            new TestCaseData(
                new List<LabeledTextSimilarityIndex>() {
                    new LabeledTextSimilarityIndex(1, "sv", 0.62) { },
                    new LabeledTextSimilarityIndex(2, "en", 0.24) { },
                    new LabeledTextSimilarityIndex(3, "en", 0.12) { }
                },
                OutcomeBuilder.CreateSuccess(
                    "All the unique labels in the provided list of LabeledTextSimilarityIndex objects have been extracted.",
                    new List<string>() { "sv", "en" }).Get()
            ).SetName(nameof(PrivateExtractUniqueLabels_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {01}"),

            new TestCaseData(
                new List<LabeledTextSimilarityIndex>(),
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to extract the unique labels in the provided list of LabeledTextSimilarityIndex objects."
                    }).Get()
            ).SetName(nameof(PrivateExtractUniqueLabels_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {02}"),

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to extract the unique labels in the provided list of LabeledTextSimilarityIndex objects."
                    }).Get()
            ).SetName(nameof(PrivateExtractUniqueLabels_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {03}")

        };
        private static TestCaseData[] _arrPrivateExtractSimilarityIndexes =
        {

            new TestCaseData(
                "en",
                new List<LabeledTextSimilarityIndex>() {
                    new LabeledTextSimilarityIndex(1, "sv", 0.62) { },
                    new LabeledTextSimilarityIndex(2, "en", 0.24) { },
                    new LabeledTextSimilarityIndex(3, "en", 0.12) { }
                },
                OutcomeBuilder.CreateSuccess(
                    "The similarity indexes in the provided list of LabeledTextSimilarityIndex objects have been extracted.",
                    new List<double>() { 0.24, 0.12 }).Get()
            ).SetName(nameof(PrivateExtractSimilarityIndexes_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {01}"),

            new TestCaseData(
                null,
                new List<LabeledTextSimilarityIndex>() {
                    new LabeledTextSimilarityIndex(1, "sv", 0.62) { },
                    new LabeledTextSimilarityIndex(2, "en", 0.24) { },
                    new LabeledTextSimilarityIndex(3, "en", 0.12) { }
                },
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to extract the similarity indexes in the provided list of LabeledTextSimilarityIndex objects." }
                    ).Get()
            ).SetName(nameof(PrivateExtractSimilarityIndexes_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {02}"),

            new TestCaseData(
                "en",
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 1.ToString()),
                        "It hasn't been possible to extract the similarity indexes in the provided list of LabeledTextSimilarityIndex objects." }).Get()
            ).SetName(nameof(PrivateExtractSimilarityIndexes_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {03}"),

            new TestCaseData(
                "dk",
                new List<LabeledTextSimilarityIndex>() {
                    new LabeledTextSimilarityIndex(1, "sv", 0.62) { },
                    new LabeledTextSimilarityIndex(2, "en", 0.24) { },
                    new LabeledTextSimilarityIndex(3, "en", 0.12) { }
                },
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        "No entries found for the provided label in the provided list of LabeledTextSimilarityIndex objects.",
                        "It hasn't been possible to extract the similarity indexes in the provided list of LabeledTextSimilarityIndex objects." }
                    ).Get()
            ).SetName(nameof(PrivateExtractSimilarityIndexes_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {04}")

        };
        private static TestCaseData[] _arrPrivateGetHighestAverage =
        {

            new TestCaseData(
                new List<LabeledTextSimilarityAverage>() {
                    new LabeledTextSimilarityAverage("sv", 0.19),
                    new LabeledTextSimilarityAverage("en", 0.45)
                },
                OutcomeBuilder.CreateSuccess(
                    new List<string>() {
                        "The highest average in the provided list of LabeledTextSimilarityIndex objects have been extracted." },
                    new LabeledTextSimilarityAverage("en", 0.45)).Get()
            ).SetName(nameof(PrivateGetHighestAverage_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {01}"),

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to extract the highest average in the provided list of LabeledTextSimilarityIndex objects." }
                    ).Get()
            ).SetName(nameof(PrivateGetHighestAverage_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {02}"),

            new TestCaseData(
                new List<LabeledTextSimilarityAverage>(),
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to extract the highest average in the provided list of LabeledTextSimilarityIndex objects." }
                    ).Get()
            ).SetName(nameof(PrivateGetHighestAverage_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {03}")

        };
        private static TestCaseData[] _arrGetLabeledTextsNullEmptyPaths =
        {

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to obtain the labeled text(s) for the provided file path." }).Get()
            )

        };
        private static TestCaseData[] _arrConvertToNGramsLabeledTexts = 
        {

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to create the NGrammed version of the provided list of labeled texts." }).Get()
            ).SetName(nameof(ConvertToNGrams_ShouldReturnTheExpectedOutcome_WhenInvokedForListLabeledTextJson) + " {01}"),

            new TestCaseData(
                new List<LabeledTextJson>(),
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to create the NGrammed version of the provided list of labeled texts." }).Get()
            ).SetName(nameof(ConvertToNGrams_ShouldReturnTheExpectedOutcome_WhenInvokedForListLabeledTextJson) + " {02}"),

            new TestCaseData(
                new List<LabeledTextJson>() {
                    new LabeledTextJson() {
                        LabeledTextId = 1,
                        Label = "sv",
                        Text = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö." },
                    new LabeledTextJson() {
                        LabeledTextId = 2,
                        Label = "en",
                        Text = "We are looking for several skilled and driven developers to join our team in Lund." },
                },
                OutcomeBuilder.CreateSuccess(
                    "The NGrammed version of the provided list of labeled texts has been successfully created.",
                    new List<LabeledTextNGrams>() {
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
                }).Get()
            ).SetName(nameof(ConvertToNGrams_ShouldReturnTheExpectedOutcome_WhenInvokedForListLabeledTextJson) + " {03}")

        };
        private static TestCaseData[] _arrConvertToNGramsText = 
        {

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to create the NGrammed version of the provided text." }).Get()
            ).SetName(nameof(ConvertToNGrams_ShouldReturnTheExpectedOutcome_WhenInvokedForText) + " {01}"),

            new TestCaseData(
                String.Empty,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to create the NGrammed version of the provided text." }).Get()
            ).SetName(nameof(ConvertToNGrams_ShouldReturnTheExpectedOutcome_WhenInvokedForText) + " {02}"),

            new TestCaseData(
                "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.",
                OutcomeBuilder.CreateSuccess(
                    new List<string>() {
                        "The provided text has been successfully tokenized.",
                        "The NGrammed version of the provided text has been successfully created."
                    },
                    new List<string>() {
                            "vår kund erbjuder",
                            "kund erbjuder trivsel",
                            "erbjuder trivsel arbetsglädje",
                            "trivsel arbetsglädje och",
                            "arbetsglädje och en",
                            "och en trygg",
                            "en trygg arbetsmiljö",
                            "trygg arbetsmiljö",
                            "arbetsmiljö"
                        }).Get()
            ).SetName(nameof(ConvertToNGrams_ShouldReturnTheExpectedOutcome_WhenInvokedForText) + " {03}")

        };
        private static TestCaseData[] _arrGetSimilarityIndexes =
        {

            new TestCaseData(
                new List<string>() {
                        "vår kund erbjuder",
                        "kund erbjuder trivsel",
                        "erbjuder trivsel arbetsglädje",
                        "trivsel arbetsglädje och",
                        "arbetsglädje och en",
                        "och en trygg",
                        "en trygg arbetsmiljö",
                        "trygg arbetsmiljö",
                        "arbetsmiljö"
                    },
                new List<LabeledTextNGrams>() {
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
                    })},
                OutcomeBuilder.CreateSuccess(
                    "The list containining the similarities between the provided NGrammed text and each of the labeled texts has been successfully created.",
                    new List<LabeledTextSimilarityIndex>() {
                        new LabeledTextSimilarityIndex(1, "sv", 1.00),
                        new LabeledTextSimilarityIndex(2, "en", 0.00)
                    }
                ).Get()
            ).SetName(nameof(GetSimilarityIndexes_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {01}"),

            new TestCaseData(
                new List<string>() {
                        "vår kund erbjuder",
                        "kund erbjuder trivsel",
                        "erbjuder trivsel arbetsglädje",
                        "trivsel arbetsglädje och",
                        "arbetsglädje och en",
                        "och en trygg",
                        "en trygg arbetsmiljö",
                        "trygg arbetsmiljö",
                        "arbetsmiljö"
                    },
                new List<LabeledTextNGrams>() {
                    new LabeledTextNGrams(1, "sv", null )},
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 1.ToString()),
                        "It hasn't been possible to calculate the Jaccard Index out of the provided Lists.",
                        "It hasn't been possible to create the list containining the similarities between the provided NGrammed text and each of the labeled texts." }).Get()
                ).SetName(nameof(GetSimilarityIndexes_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {02}"),

            new TestCaseData(
                new List<string>(),
                new List<LabeledTextNGrams>(),
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to create the list containining the similarities between the provided NGrammed text and each of the labeled texts." })
                .Get()
            ).SetName(nameof(GetSimilarityIndexes_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {03}"),

            new TestCaseData(
                null,
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to create the list containining the similarities between the provided NGrammed text and each of the labeled texts." })
                .Get()
            ).SetName(nameof(GetSimilarityIndexes_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {04}")

        };
        private static TestCaseData[] _arrGetSimilarityAverages = 
        {

            new TestCaseData(
                new List<LabeledTextSimilarityIndex>() {
                    new LabeledTextSimilarityIndex(1, "sv", 0.62) { },
                    new LabeledTextSimilarityIndex(2, "en", 0.24) { },
                    new LabeledTextSimilarityIndex(3, "en", 0.12) { }
                },
                OutcomeBuilder.CreateSuccess(
                    "A list containing the average similarity index for each unique label has been successfully created.",
                    new List<LabeledTextSimilarityAverage>() {
                        new LabeledTextSimilarityAverage("sv", 0.62),
                        new LabeledTextSimilarityAverage("en", 0.18),
                    }
                ).Get()
            ).SetName(nameof(GetSimilarityAverages_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {01}"),

            new TestCaseData(
                new List<LabeledTextSimilarityIndex>() {
                    new LabeledTextSimilarityIndex(1, null, 0.62) { },
                    new LabeledTextSimilarityIndex(2, "en", 0.24) { },
                    new LabeledTextSimilarityIndex(3, "en", 0.12) { }
                },
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to extract the similarity indexes in the provided list of LabeledTextSimilarityIndex objects.",
                        "It hasn't been possible to create a list containing the average similarity index for each unique label." }).Get()
            ).SetName(nameof(GetSimilarityAverages_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {02}"),

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to create a list containing the average similarity index for each unique label." }).Get()
            ).SetName(nameof(GetSimilarityAverages_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {03}"),

            new TestCaseData(
                new List<LabeledTextSimilarityIndex>(),
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to create a list containing the average similarity index for each unique label." }).Get()
            ).SetName(nameof(GetSimilarityAverages_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {04}")

        };
        private static TestCaseData[] _arrEstimateLabel = 
        {

            new TestCaseData(
                new List<LabeledTextSimilarityAverage>() {
                    new LabeledTextSimilarityAverage("sv", 0.62),
                    new LabeledTextSimilarityAverage("en", 0.18),
                },
                OutcomeBuilder.CreateSuccess(
                   "The label has been estimated according to the provided list of average similarity indexes.",
                    "sv").Get()
            ).SetName(nameof(EstimateLabel_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {01}"),

            new TestCaseData(
                new List<LabeledTextSimilarityAverage>() {
                    new LabeledTextSimilarityAverage("sv", 0),
                    new LabeledTextSimilarityAverage("en", 0),
                },
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        "All the provided averages are equal to zero.",
                        "It hasn't been possible to extract the highest average in the provided list of LabeledTextSimilarityIndex objects.",
                        "It hasn't been possible to estimate the label according to the provided list of average similarity indexes."
                    }).Get()
            ).SetName(nameof(EstimateLabel_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {02}"),

            new TestCaseData(
                new List<LabeledTextSimilarityAverage>() {
                    new LabeledTextSimilarityAverage("sv", 0.04),
                    new LabeledTextSimilarityAverage("en", 0.04),
                    new LabeledTextSimilarityAverage("dk", 0.04)
                },
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        "All the provided averages are equal each other.",
                        "It hasn't been possible to extract the highest average in the provided list of LabeledTextSimilarityIndex objects.",
                        "It hasn't been possible to estimate the label according to the provided list of average similarity indexes."
                    }).Get()
            ).SetName(nameof(EstimateLabel_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {03}"),

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to estimate the label according to the provided list of average similarity indexes." }).Get()
            ).SetName(nameof(EstimateLabel_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {04}"),

            new TestCaseData(
                new List<LabeledTextSimilarityAverage>(),
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        String.Format(_errParameterValidator, 0.ToString()),
                        "It hasn't been possible to estimate the label according to the provided list of average similarity indexes." }).Get()
            ).SetName(nameof(EstimateLabel_ShouldReturnTheExpectedOutcome_WhenInvoked) + " {05}")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(_arrFormatAsTableList))]
        public void FormatAsTable_ShouldReturnTheExpectedOutcome_WhenListILabeledTextSimilarityValue(
            List<ILabeledTextSimilarityValue> listSimilarityValues, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new NGramsTextClassifier().FormatAsTable(listSimilarityValues);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }
        [TestCaseSource(nameof(_arrFormatAsTable))]
        public void FormatAsTable_ShouldReturnTheExpectedOutcome_WhenILabeledTextSimilarityValue(
            ILabeledTextSimilarityValue objSimilarityValue, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new NGramsTextClassifier().FormatAsTable(objSimilarityValue);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }
        [Test]
        public void FormatAsTable_ShouldThrowAnException_WhenListButParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { ParametersValidator = null };

            // Act
            Outcome objActual = objClassifier.FormatAsTable(new List<ILabeledTextSimilarityValue>());

            // Assert
            Assert.IsTrue(objActual.IsException());

        }
        [Test]
        public void FormatAsTable_ShouldThrowAnException_WhenILabeledTextSimilarityValueButParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { ParametersValidator = null };

            // Act
            Outcome objActual = objClassifier.FormatAsTable(new LabeledTextSimilarityIndex(3, "sv", 0.3));

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        [TestCaseSource(nameof(_arrPrivateCalculateAverage))]
        public void PrivateCalculateAverage_ShouldReturnTheExpectedOutcome_WhenInvoked
            (List<double> listAverages, Outcome objExpected)
        {

            // Arrange
            MethodInfo CalculateAverage = new NUnitHelpers().GetPrivateMethod(
                new NGramsTextClassifier(), nameof(CalculateAverage));

            // Act
            Outcome objActual = CalculateAverage.Invoke(new NGramsTextClassifier(), new object[] { listAverages }) as Outcome;

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }
        [Test]
        public void PrivateCalculateAverage_ShouldThrowAnException_WhenParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() {
                ParametersValidator = null };
            MethodInfo CalculateAverage = new NUnitHelpers().GetPrivateMethod(
                objClassifier, nameof(CalculateAverage));
            List<double> listAverages = new List<double>() { 0.19, 0.45 };

            // Act
            Outcome objActual = CalculateAverage.Invoke(objClassifier, new object[] { listAverages }) as Outcome;

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        [TestCaseSource(nameof(_arrPrivateExtractUniqueLabels))]
        public void PrivateExtractUniqueLabels_ShouldReturnTheExpectedOutcome_WhenInvoked
            (List<LabeledTextSimilarityIndex> listSimilarityIndexes, Outcome objExpected)
        {

            // Arrange
            MethodInfo ExtractUniqueLabels = new NUnitHelpers().GetPrivateMethod(
                new NGramsTextClassifier(), nameof(ExtractUniqueLabels));

            // Act
            Outcome objActual = ExtractUniqueLabels.Invoke(new NGramsTextClassifier(), new object[] { listSimilarityIndexes }) as Outcome;

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }
        [Test]
        public void PrivateExtractUniqueLabels_ShouldThrowAnException_WhenParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() {
                ParametersValidator = null };
            MethodInfo ExtractUniqueLabels = new NUnitHelpers().GetPrivateMethod(
                objClassifier, nameof(ExtractUniqueLabels));
            List<LabeledTextSimilarityIndex> listSimilarityIndexes = new List<LabeledTextSimilarityIndex>() {
                    new LabeledTextSimilarityIndex(1, "sv", 0.62) { },
                    new LabeledTextSimilarityIndex(2, "en", 0.24) { },
                    new LabeledTextSimilarityIndex(3, "en", 0.12) { }
            };

            // Act
            Outcome objActual = ExtractUniqueLabels.Invoke(objClassifier, new object[] { listSimilarityIndexes }) as Outcome;

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        [TestCaseSource(nameof(_arrPrivateExtractSimilarityIndexes))]
        public void PrivateExtractSimilarityIndexes_ShouldReturnTheExpectedOutcome_WhenInvoked
            (string strLabel, List<LabeledTextSimilarityIndex> listSimilarityIndexes, Outcome objExpected)
        {

            // Arrange
            MethodInfo ExtractSimilarityIndexes = new NUnitHelpers().GetPrivateMethod(
                new NGramsTextClassifier(), nameof(ExtractSimilarityIndexes));

            // Act
            Outcome objActual = ExtractSimilarityIndexes.Invoke(
                new NGramsTextClassifier(), new object[] { strLabel, listSimilarityIndexes }) as Outcome;

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }
        [Test]
        public void PrivateExtractSimilarityIndexes_ShouldThrowAnException_WhenParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() {
                ParametersValidator = null };
            MethodInfo ExtractSimilarityIndexes = new NUnitHelpers().GetPrivateMethod(
                objClassifier, nameof(ExtractSimilarityIndexes));
            string strLabel = "sv";
            List<LabeledTextSimilarityIndex> listSimilarityIndexes = new List<LabeledTextSimilarityIndex>() {
                    new LabeledTextSimilarityIndex(1, "sv", 0.62) { },
                    new LabeledTextSimilarityIndex(2, "en", 0.24) { },
                    new LabeledTextSimilarityIndex(3, "en", 0.12) { }
            };

            // Act
            Outcome objActual = ExtractSimilarityIndexes.Invoke(objClassifier, new object[] { strLabel, listSimilarityIndexes }) as Outcome;

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        [TestCaseSource(nameof(_arrPrivateGetHighestAverage))]
        public void PrivateGetHighestAverage_ShouldReturnTheExpectedOutcome_WhenInvoked
            (List<LabeledTextSimilarityAverage> listSimilarityAverages, Outcome objExpected)
        {

            // Arrange
            MethodInfo GetHighestAverage = new NUnitHelpers().GetPrivateMethod(
                new NGramsTextClassifier(), nameof(GetHighestAverage));

            // Act
            Outcome objActual = GetHighestAverage.Invoke(
                new NGramsTextClassifier(), new object[] { listSimilarityAverages }) as Outcome;

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            if (objActual.Result != null)
            {

                Assert.AreEqual(
                    ((LabeledTextSimilarityAverage)objExpected.Result).Label,
                    ((LabeledTextSimilarityAverage)objActual.Result).Label);
                Assert.AreEqual(
                    ((LabeledTextSimilarityAverage)objExpected.Result).Average,
                    ((LabeledTextSimilarityAverage)objActual.Result).Average);

            }

        }
        [Test]
        public void PrivateGetHighestAverage_ShouldThrowAnException_WhenParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { ParametersValidator = null };
            MethodInfo GetHighestAverage = new NUnitHelpers().GetPrivateMethod(
                objClassifier, nameof(GetHighestAverage));
            List<LabeledTextSimilarityAverage> listSimilarityAverages = new List<LabeledTextSimilarityAverage>() {
                    new LabeledTextSimilarityAverage("sv", 0.19),
                    new LabeledTextSimilarityAverage("en", 0.45)
                };

            // Act
            Outcome objActual = GetHighestAverage.Invoke(objClassifier, new object[] { listSimilarityAverages }) as Outcome;

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        [TestCaseSource(nameof(_arrGetLabeledTextsNullEmptyPaths))]
        public void GetLabeledTexts_ShouldReturnTheExpectedOutcome_WhenFilePathIsNullOrEmpty
            (TextFile objTextFile, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new NGramsTextClassifier().GetLabeledTexts(objTextFile);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }
        [Test]
        public void GetLabeledTexts_ShouldReturnTheExpectedFailureOutcome_WhenDoesFilePathExistFails()
        {

            // Arrange
            string strFilePath = @"C:\\gibberish\\path\\LabeledTextsJson.txt";
            Outcome objExpected = new Outcome(
                    OutcomeStatuses.Failure,
                    new List<string>() {
                        "The provided file path ('" + strFilePath + "') doesn't exist.",
                        "It hasn't been possible to obtain the labeled text(s) for the provided file path." },
                    null);
            ITextFile fakeTextFile = Substitute.For<ITextFile>();
            fakeTextFile.FilePath.Returns(strFilePath);
            fakeTextFile.DoesFilePathExist().Returns(objExpected);
            NGramsTextClassifier objClassifier = new NGramsTextClassifier();

            // Act
            Outcome objActual = objClassifier.GetLabeledTexts(fakeTextFile);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }
        [Test]
        public void GetLabeledTexts_ShouldReturnTheExpectedFailureOutcome_WhenReadFails()
        {

            // Arrange
            Uri objFilePath = new Uri("C:\\gibberish\\path\\LabeledTextsJson.txt");
            Outcome objExpected = new Outcome(
                    OutcomeStatuses.Failure,
                    new List<string>() {
                        "fakeexception",
                        "It hasn't been possible to obtain the labeled text(s) for the provided file path." },
                    null);
            ITextFile fakeTextFile = Substitute.For<ITextFile>();
            fakeTextFile.DoesFilePathExist().Returns(OutcomeBuilder.CreateSuccess("fakesuccessmessage", null).Get());
            fakeTextFile.Read().Returns(OutcomeBuilder.CreateFailure("fakeexception").Get());
            NGramsTextClassifier objClassifier = new NGramsTextClassifier();

            // Act
            Outcome objActual = objClassifier.GetLabeledTexts(fakeTextFile);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }
        [Test]
        public void GetLabeledTexts_ShouldThrowAnException_WhenLabeledTextJsonDeserializerThrowsAnException()
        {

            // Arrange
            Uri objFilePath = new Uri("C:\\gibberish\\path\\LabeledTextsJson.txt");
            ITextFile fakeTextFile = Substitute.For<ITextFile>();
            fakeTextFile.DoesFilePathExist().Returns(OutcomeBuilder.CreateSuccess("fakesuccessmessage", null).Get());
            fakeTextFile.Read().Returns(OutcomeBuilder.CreateSuccess("fakesuccessmessage", "unproperjson").Get());
            NGramsTextClassifier objClassifier = new NGramsTextClassifier();
 
            // Act
            Outcome objActual = objClassifier.GetLabeledTexts(fakeTextFile);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }
        [Test]
        public void GetLabeledTexts_ShouldReturnTheExpectedSuccessOutcome_WhenLabeledTextJsonDeserializerSucceed()
        {

            // Arrange
            Uri objFilePath = new Uri("C:\\gibberish\\path\\LabeledTextsJson.txt");
            Outcome objExpected = OutcomeBuilder.CreateSuccess(
                new List<string>() {
                    "The labeled text(s) for the provided file path has been obtained." },
                new List<LabeledTextJson>() {
                    new LabeledTextJson() {
                        LabeledTextId = 1,
                        Label = "sv",
                        Text = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö." },
                    new LabeledTextJson() {
                        LabeledTextId = 2,
                        Label = "en",
                        Text = "We are looking for several skilled and driven developers to join our team in Lund." },
                }).Get();
            ITextFile fakeTextFile = Substitute.For<ITextFile>();
            fakeTextFile.DoesFilePathExist().Returns(OutcomeBuilder.CreateSuccess("fakesuccessmessage", null).Get());
            fakeTextFile.Read().Returns(OutcomeBuilder.CreateSuccess("fakesuccessmessage", _strLabeledTextJson).Get());
            NGramsTextClassifier objClassifier = new NGramsTextClassifier();

            // Act
            Outcome objActual = objClassifier.GetLabeledTexts(fakeTextFile);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            for (int i = 0; i < ((List<LabeledTextJson>)objExpected.Result).Count; i++)
            {
                Assert.AreEqual(((List<LabeledTextJson>)objExpected.Result)[i].LabeledTextId, ((List<LabeledTextJson>)objActual.Result)[i].LabeledTextId);
                Assert.AreEqual(((List<LabeledTextJson>)objExpected.Result)[i].Label, ((List<LabeledTextJson>)objActual.Result)[i].Label);
                Assert.AreEqual(((List<LabeledTextJson>)objExpected.Result)[i].Text, ((List<LabeledTextJson>)objActual.Result)[i].Text);
            }

        }
        [Test]
        public void GetLabeledTexts_ShouldReturnTheExpectedFailureOutcome_WhenNullTextFile()
        {

            // Arrange
            TextFile objTextFile = new TextFile("C:\\gibberish\\path\\LabeledTextsJson.txt");
            objTextFile = null;

            // Act
            Outcome objActual = new NGramsTextClassifier().GetLabeledTexts(objTextFile);

            // Assert
            Assert.IsTrue(objActual.IsFailure());

        }
        [Test]
        public void GetLabeledTexts_ShouldThrowAnException_WhenTextFileIsProperButParametersValidatorIsNull()
        {

            // Arrange
            TextFile objTextFile = new TextFile("C:\\gibberish\\path\\LabeledTextsJson.txt");
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { ParametersValidator = null };

            // Act
            Outcome objActual = objClassifier.GetLabeledTexts(objTextFile);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }
        [Test]
        public void GetLabeledTexts_ShouldReturnTheExpectedFailureOutcome_WhenEmptyStringAsJson()
        {

            // Arrange
            // Act
            Outcome objActual = new NGramsTextClassifier().GetLabeledTexts(String.Empty);

            // Assert
            Assert.IsTrue(objActual.IsFailure());

        }
        [Test]
        public void GetLabeledTexts_ShouldThrowAnException_WhenJsonButParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { ParametersValidator = null };

            // Act
            Outcome objActual = objClassifier.GetLabeledTexts("somejson");

            // Assert
            Assert.IsTrue(objActual.IsException());

        }
        [Test]
        public void GetLabeledTexts_ShouldReturnTheExpectedSuccessOutcome_WhenProperJson()
        {

            // Arrange
            string strLabeledTextJson = 
                "[ { \"LabeledTextId\": 1, \"Label\": \"sv\", \"Text\": \"Vår kund erbjuder...\" } ]";

            // Act
            Outcome objActual = new NGramsTextClassifier().GetLabeledTexts(strLabeledTextJson);

            // Assert
            Assert.AreEqual(OutcomeStatuses.Success, objActual.Status);

        }

        [TestCaseSource(nameof(_arrConvertToNGramsLabeledTexts))]
        public void ConvertToNGrams_ShouldReturnTheExpectedOutcome_WhenInvokedForListLabeledTextJson
            (List<LabeledTextJson> listLabeledTexts, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new NGramsTextClassifier().ConvertToNGrams(listLabeledTexts, _objTokenizationStrategy);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);

            if (objActual.Result != null)
                for (int i = 0; i < ((List<LabeledTextNGrams>)objExpected.Result).Count; i++)
                {
                    Assert.AreEqual(((List<LabeledTextNGrams>)objExpected.Result)[i].LabeledTextId, ((List<LabeledTextNGrams>)objActual.Result)[i].LabeledTextId);
                    Assert.AreEqual(((List<LabeledTextNGrams>)objExpected.Result)[i].Label, ((List<LabeledTextNGrams>)objActual.Result)[i].Label);
                    Assert.AreEqual(((List<LabeledTextNGrams>)objExpected.Result)[i].NGrams, ((List<LabeledTextNGrams>)objActual.Result)[i].NGrams);
                }

        }
        [Test]
        public void ConvertToNGrams_ShouldThrowAnException_WhenInvokedForListLabeledTextJsonAndParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { ParametersValidator = null };
            List<LabeledTextJson> listLabeledTexts = new List<LabeledTextJson>() {
                    new LabeledTextJson() {
                        LabeledTextId = 1,
                        Label = "sv",
                        Text = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö." },
                    new LabeledTextJson() {
                        LabeledTextId = 2,
                        Label = "en",
                        Text = "We are looking for several skilled and driven developers to join our team in Lund." },
                };

            // Act
            Outcome objActual = objClassifier.ConvertToNGrams(listLabeledTexts, _objTokenizationStrategy);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }
        [Test]
        public void ConvertToNGrams_ShouldReturnFailure_WhenInvokedForListLabeledTextJsonAndTokenizationStrategiesAreNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier();
            List<LabeledTextJson> listLabeledTexts = new List<LabeledTextJson>() {
                    new LabeledTextJson() {
                        LabeledTextId = 1,
                        Label = "sv",
                        Text = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö." },
                    new LabeledTextJson() {
                        LabeledTextId = 2,
                        Label = "en",
                        Text = "We are looking for several skilled and driven developers to join our team in Lund." },
                };

            // Act
            Outcome objActual = objClassifier.ConvertToNGrams(listLabeledTexts, new List<ITokenizationStrategy>());

            // Assert
            Assert.IsTrue(objActual.IsFailure());

        }
        [Test]
        public void ConvertToNGrams_ShouldThrowAnException_WhenInvokedForListLabeledTextJsonAndNGramsTokenizerReturlsFailure()
        {

            // Arrange
            ITokenizationStrategy objTokenizationStrategy = new TokenizationStrategyTrigrams() { Pattern = "{1,}" };
            NGramsTextClassifier objClassifier = new NGramsTextClassifier();
            List<LabeledTextJson> listLabeledTexts = new List<LabeledTextJson>() {
                    new LabeledTextJson() {
                        LabeledTextId = 1,
                        Label = "sv",
                        Text = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö." },
                    new LabeledTextJson() {
                        LabeledTextId = 2,
                        Label = "en",
                        Text = "We are looking for several skilled and driven developers to join our team in Lund." },
                };

            // Act
            Outcome objActual = objClassifier.ConvertToNGrams(listLabeledTexts, objTokenizationStrategy);

            // Assert
            Assert.IsTrue(objActual.IsException());
            Assert.AreEqual(
                "It hasn't been possible to create the NGrammed version of the provided list of labeled texts.",
                objActual.Messages[2]);

        }
        [TestCaseSource(nameof(_arrConvertToNGramsText))]
        public void ConvertToNGrams_ShouldReturnTheExpectedOutcome_WhenInvokedForText
            (string strText, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new NGramsTextClassifier().ConvertToNGrams(strText, _objTokenizationStrategy);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }
        [Test]
        public void ConvertToNGrams_ShouldThrowAnException_WhenInvokedForTextAndParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { ParametersValidator = null };
            string strText = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.";

            // Act
            Outcome objActual = objClassifier.ConvertToNGrams(strText, _objTokenizationStrategy);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }
        [Test]
        public void ConvertToNGrams_ShouldReturnFailure_WhenInvokedForTextAndTokenizationStrategiesAreNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier(); ;
            string strText = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.";

            // Act
            Outcome objActual = objClassifier.ConvertToNGrams(strText, new List<ITokenizationStrategy>());

            // Assert
            Assert.IsTrue(objActual.IsFailure());

        }
        [Test]
        public void ConvertToNGrams_ShouldReturnTheExpectedExceptionOutcome_WhenInvokedForTextAndNGramsTokenizerReturlsFailure()
        {

            // Arrange
            ITokenizationStrategy objTokenizationStrategy = new TokenizationStrategyTrigrams() { Pattern = "{1,}" };
            NGramsTextClassifier objClassifier = new NGramsTextClassifier();
            string strText = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.";

            // Act
            Outcome objActual = objClassifier.ConvertToNGrams(strText, objTokenizationStrategy);

            // Assert
            Assert.AreEqual(OutcomeStatuses.Exception, objActual.Status);
            Assert.AreEqual(
                "It hasn't been possible to create the NGrammed version of the provided text.",
                objActual.Messages[2]);

        }

        [TestCaseSource(nameof(_arrGetSimilarityIndexes))]
        public void GetSimilarityIndexes_ShouldReturnTheExpectedOutcome_WhenInvoked
            (List<string> strTextNGrams, List<LabeledTextNGrams> listLabeledTextsNGrams, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new NGramsTextClassifier().GetSimilarityIndexes(strTextNGrams, listLabeledTextsNGrams);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);

            if (objActual.Result != null)
                for (int i = 0; i < ((List<LabeledTextSimilarityIndex>)objExpected.Result).Count; i++)
                {
                    Assert.AreEqual(
                        ((List<LabeledTextSimilarityIndex>)objExpected.Result)[i].LabeledTextId, 
                        ((List<LabeledTextSimilarityIndex>)objActual.Result)[i].LabeledTextId);
                    Assert.AreEqual(
                        ((List<LabeledTextSimilarityIndex>)objExpected.Result)[i].Label, 
                        ((List<LabeledTextSimilarityIndex>)objActual.Result)[i].Label);
                    Assert.AreEqual(
                        ((List<LabeledTextSimilarityIndex>)objExpected.Result)[i].SimilarityIndex, 
                        ((List<LabeledTextSimilarityIndex>)objActual.Result)[i].SimilarityIndex);
                }

        }
        [Test]
        public void GetSimilarityIndexes_ShouldThrowAnException_WhenParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { ParametersValidator = null };
            List<string> strTextNGrams = new List<string>() {
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
            List<LabeledTextNGrams> listLabeledTextsNGrams = new List<LabeledTextNGrams>() {
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
                    })};

            // Act
            Outcome objActual = objClassifier.GetSimilarityIndexes(strTextNGrams, listLabeledTextsNGrams);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }
        [Test]
        public void GetSimilarityIndexes_ShouldThrowAnException_WhenNGramsSimilarityCalculatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { NGramsSimilarityCalculator = null };
            List<string> strTextNGrams = new List<string>() {
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
            List<LabeledTextNGrams> listLabeledTextsNGrams = new List<LabeledTextNGrams>() {
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
                    })};

            // Act
            Outcome objActual = objClassifier.GetSimilarityIndexes(strTextNGrams, listLabeledTextsNGrams);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }
        [TestCaseSource(nameof(_arrGetSimilarityAverages))]
        public void GetSimilarityAverages_ShouldReturnTheExpectedOutcome_WhenInvoked
            (List<LabeledTextSimilarityIndex> listSimilarityIndexes, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new NGramsTextClassifier().GetSimilarityAverages(listSimilarityIndexes);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);

            if (objActual.Result != null)
                for (int i = 0; i < ((List<LabeledTextSimilarityAverage>)objExpected.Result).Count; i++)
                {
                    Assert.AreEqual(
                        ((List<LabeledTextSimilarityAverage>)objExpected.Result)[i].Label,
                        ((List<LabeledTextSimilarityAverage>)objActual.Result)[i].Label);
                    Assert.AreEqual(
                        ((List<LabeledTextSimilarityAverage>)objExpected.Result)[i].Average,
                        ((List<LabeledTextSimilarityAverage>)objActual.Result)[i].Average);
                }

        }
        [Test]
        public void GetSimilarityAverages_ShouldThrowAnException_WhenParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { ParametersValidator = null };
            List<LabeledTextSimilarityIndex> listSimilarityIndexes =
                new List<LabeledTextSimilarityIndex>() {
                    new LabeledTextSimilarityIndex(1, "sv", 0.62) { },
                    new LabeledTextSimilarityIndex(2, "en", 0.24) { },
                    new LabeledTextSimilarityIndex(3, "en", 0.12) { }
                };

            // Act
            Outcome objActual = objClassifier.GetSimilarityAverages(listSimilarityIndexes);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        [TestCaseSource(nameof(_arrEstimateLabel))]
        public void EstimateLabel_ShouldReturnTheExpectedOutcome_WhenInvoked
            (List<LabeledTextSimilarityAverage> listSimilarityAverages, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new NGramsTextClassifier().EstimateLabel(listSimilarityAverages);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            for (int i = 0; i < objExpected.Messages.Count; i++)
                Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }
        [Test]
        public void EstimateLabel_ShouldThrowAnException_WhenParametersValidatorIsNull()
        {

            // Arrange
            NGramsTextClassifier objClassifier = new NGramsTextClassifier() { ParametersValidator = null };
            List<LabeledTextSimilarityAverage> listSimilarityAverages =
                new List<LabeledTextSimilarityAverage>() {
                    new LabeledTextSimilarityAverage("sv", 0.62),
                    new LabeledTextSimilarityAverage("en", 0.18),
                };

            // Act
            Outcome objActual = objClassifier.EstimateLabel(listSimilarityAverages);

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
