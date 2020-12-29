using System;
using System.Collections.Generic;
using System.Linq;
using RUBN.Shared;

namespace NW.NGrams
{
    public class NGramsTextClassifier : INGramsTextClassifier
    {

        // Fields
        // Properties
        public Func<double, double> RoundingStrategy { get; } = RoundingStategies.SixDecimalDigits;
        public ILabeledTextJsonDeserializer LabeledTextJsonDeserializer { get; set; } = new LabeledTextJsonDeserializer();
        public INGramsTokenizer NGramsTokenizer { get; set; } = new NGramsTokenizer();
        public INGramsSimilarityCalculator NGramsSimilarityCalculator { get; set; } = new JaccardIndexCalculator();
        public IParametersValidator ParametersValidator { get; set; } = new ParametersValidator();
        public Func<List<LabeledTextSimilarityAverage>, Boolean> AreAllZerosStrategy { get; } = HighestAverageStrategies.AreAllZeros;
        public Func<List<LabeledTextSimilarityAverage>, Boolean> AreDistinctStrategy { get; } = HighestAverageStrategies.AreDistinct;
        public Func<List<LabeledTextSimilarityAverage>, LabeledTextSimilarityAverage> GetHighestStrategy { get; } = HighestAverageStrategies.GetHighest;

        // Constructors
        public NGramsTextClassifier() { }

        // Methods (public)
        public Outcome GetLabeledTexts(ITextFile objTextFile)
        {

            string msgSuccess = "The labeled text(s) for the provided file path has been obtained.";
            string errFailure = "It hasn't been possible to obtain the labeled text(s) for the provided file path.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(objTextFile);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                objReturn = objTextFile.DoesFilePathExist();
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                objReturn = objTextFile.Read();
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                string strLabeledTextJson = objReturn.Result.ToString();
                objReturn = GetLabeledTexts(strLabeledTextJson);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                return OutcomeBuilder.CreateSuccess(msgSuccess, objReturn.Result).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        public Outcome GetLabeledTexts(string strLabeledTextJson)
        {

            string msgSuccess = "The labeled text(s) have been obtained from the provided string.";
            string errFailure = "It hasn't been possible to obtain the labeled text(s) from the provided string.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(strLabeledTextJson);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                objReturn = LabeledTextJsonDeserializer.Do(strLabeledTextJson);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                return OutcomeBuilder.CreateSuccess(msgSuccess, objReturn.Result).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        public Outcome ConvertToNGrams(List<LabeledTextJson> listLabeledTexts, ITokenizationStrategy objTokenizationStrategy)
        {

            string msgSuccess = "The NGrammed version of the provided list of labeled texts has been successfully created.";
            string errFailure = "It hasn't been possible to create the NGrammed version of the provided list of labeled texts.";

            try
            {

                Outcome objReturn = ParametersValidator.AreNullOrEmpty(
                    new object[] { listLabeledTexts, objTokenizationStrategy });
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                List<LabeledTextNGrams> listLabeledTextsNGrams = new List<LabeledTextNGrams>();
                for (int i = 0; i < listLabeledTexts.Count; i++)
                {

                    objReturn = NGramsTokenizer.Do(objTokenizationStrategy, listLabeledTexts[i].Text);
                    if (objReturn.IsFailureOrException())
                        return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                    List<string> listLabeledNGrams = (List<string>)objReturn.Result;

                    listLabeledTextsNGrams.Add(new LabeledTextNGrams(
                            listLabeledTexts[i].LabeledTextId,
                            listLabeledTexts[i].Label,
                            listLabeledNGrams));

                }

                return OutcomeBuilder.CreateSuccess(msgSuccess, listLabeledTextsNGrams).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        public Outcome ConvertToNGrams(string strText, ITokenizationStrategy objTokenizationStrategy)
        {

            string msgSuccess = "The NGrammed version of the provided text has been successfully created.";
            string errFailure = "It hasn't been possible to create the NGrammed version of the provided text.";

            try
            {

                Outcome objReturn = ParametersValidator.AreNullOrEmpty( 
                    new object[] { strText, objTokenizationStrategy } );
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                objReturn = NGramsTokenizer.Do(objTokenizationStrategy, strText);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                return OutcomeBuilder.Clone(objReturn).Append(msgSuccess).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        public Outcome ConvertToNGrams(List<LabeledTextJson> listLabeledTexts, List<ITokenizationStrategy> listTokenizationStrategies)
        {

            string msgSuccess = "The NGrammed version of the provided list of labeled texts has been successfully created.";
            string errFailure = "It hasn't been possible to create the NGrammed version of the provided list of labeled texts.";

            try
            {

                Outcome objReturn = ParametersValidator.AreNullOrEmpty(
                    new object[] { listLabeledTexts, listTokenizationStrategies });
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                List<LabeledTextNGrams> listLabeledTextsNGrams = new List<LabeledTextNGrams>();
                foreach (ITokenizationStrategy objTokenizationStrategy in listTokenizationStrategies)
                {

                    objReturn = ConvertToNGrams(listLabeledTexts, objTokenizationStrategy);
                    if (objReturn.IsFailureOrException())
                        return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                    listLabeledTextsNGrams.AddRange(objReturn.Result as List<LabeledTextNGrams>);

                }

                return OutcomeBuilder.CreateSuccess(msgSuccess, listLabeledTextsNGrams).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();
            }

        }
        public Outcome ConvertToNGrams(string strText, List<ITokenizationStrategy> listTokenizationStrategies)
        {

            string msgSuccess = "The NGrammed version of the provided text has been successfully created.";
            string errFailure = "It hasn't been possible to create the NGrammed version of the provided text.";

            try
            {

                Outcome objReturn = ParametersValidator.AreNullOrEmpty(
                    new object[] { strText, listTokenizationStrategies });
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                List<string> listNGrams = new List<string>();
                foreach (ITokenizationStrategy objTokenizationStrategy in listTokenizationStrategies)
                {

                    objReturn = ConvertToNGrams(strText, objTokenizationStrategy);
                    if (objReturn.IsFailureOrException())
                        return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                    listNGrams.AddRange(objReturn.Result as List<string>);

                }

                return OutcomeBuilder.CreateSuccess(msgSuccess, listNGrams).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();
            }

        }
        public Outcome GetSimilarityIndexes(List<string> listTextNGrams, List<LabeledTextNGrams> listLabeledTextsNGrams)
        {

            /*
             * 
             * 1) It takes a text - for ex. "Vår kund erbjuder...";
             * 2) it takes a List<string> of NGrammed labeled texts - for ex.:
             *
             *      1, "sv", { "Är du genuint", "du genuint intresserad", "genuint intresserad av", ... }
             *      ...
             * 
             * 3) it returns something like: 
             * 
             *      LabeledTextId Label Similarity
             *      1               sv    0.89
             *      2               en    0.13
             *      3               en    0.45   
             *      ...
             * 
             */

            string msgSuccess = "The list containining the similarities between the provided NGrammed text and each of the labeled texts has been successfully created.";
            string errFailure = "It hasn't been possible to create the list containining the similarities between the provided NGrammed text and each of the labeled texts.";

            try
            {

                Outcome objReturn = ParametersValidator.AreNullOrEmpty(
                    new object[] { listTextNGrams, listLabeledTextsNGrams });
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                List<LabeledTextSimilarityIndex> listSimilarityIndexes = new List<LabeledTextSimilarityIndex>();
                for (int i = 0; i < listLabeledTextsNGrams.Count; i++)
                {

                    objReturn = NGramsSimilarityCalculator.Do(listTextNGrams, listLabeledTextsNGrams[i].NGrams);
                    if (objReturn.IsFailureOrException())
                        return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                    double dblSimilarity = (double)objReturn.Result;
                    listSimilarityIndexes.Add(
                        new LabeledTextSimilarityIndex(
                            listLabeledTextsNGrams[i].LabeledTextId,
                            listLabeledTextsNGrams[i].Label,
                            RoundingStrategy(dblSimilarity)
                    ));

                }

                return OutcomeBuilder.CreateSuccess(msgSuccess, listSimilarityIndexes).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        public Outcome GetSimilarityAverages(List<LabeledTextSimilarityIndex> listSimilarityIndexes)
        {

            string msgSuccess = "A list containing the average similarity index for each unique label has been successfully created.";
            string errFailure = "It hasn't been possible to create a list containing the average similarity index for each unique label.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(listSimilarityIndexes);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                objReturn = ExtractUniqueLabels(listSimilarityIndexes); // no need for IsFailure() here.
                List<string> listUniqueLabels = (List<string>)objReturn.Result;

                List<LabeledTextSimilarityAverage> listSimilarityAverages = new List<LabeledTextSimilarityAverage>();
                for (int i = 0; i < listUniqueLabels.Count; i++)
                {

                    string strLabel = listUniqueLabels[i];
                    objReturn = ExtractSimilarityIndexes(strLabel, listSimilarityIndexes);
                    if (objReturn.IsFailureOrException())
                        return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                    List<double> listIndexes = (List<double>)objReturn.Result;
                    objReturn = CalculateAverage(listIndexes); // no need for IsFailure() here.
                    double dblAverage = (double)objReturn.Result;

                    listSimilarityAverages.Add(
                        new LabeledTextSimilarityAverage(
                            strLabel,
                            RoundingStrategy(dblAverage)));

                }

                return OutcomeBuilder.CreateSuccess(msgSuccess, listSimilarityAverages).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        public Outcome EstimateLabel(List<LabeledTextSimilarityAverage> listSimilarityAverages)
        {

            /*
             * 
             * Label    Average
             * sv       0.19
             * en       0.45
             * 
             *      => { Label: "en", Average: 0.45 } => "en"
             * 
             */

            string msgSuccess = "The label has been estimated according to the provided list of average similarity indexes.";
            string errFailure = "It hasn't been possible to estimate the label according to the provided list of average similarity indexes.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(listSimilarityAverages);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                objReturn = GetHighestAverage(listSimilarityAverages);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                return OutcomeBuilder.CreateSuccess(
                    msgSuccess,
                    ((LabeledTextSimilarityAverage)objReturn.Result).Label).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        public Outcome FormatAsTable(List<ILabeledTextSimilarityValue> listSimilarityValues)
        {

            /*
             * 
             * LabeledTextId   Label    SimilarityIndex
             * 1                sv       0.21
             * 2                en       0.98
             * ..
             * 
             * or:
             * 
             * Label    Average
             * sv       0.19
             * en       0.45
             * 
             */

            string msgSuccess = "The provided list of LabeledTextSimilarityValue objects has been successfully formatted.";
            string errFailure = "It hasn't been possible to format the provided list of LabeledTextSimilarityValue objects.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(listSimilarityValues);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                string strTable = String.Empty;
                for (int i = 0; i < listSimilarityValues.Count; i++)
                {
                    if (i == 0)
                        strTable = String.Format("{0}{1}", listSimilarityValues[i].ToHeader(), Environment.NewLine);

                    if (i == (listSimilarityValues.Count - 1))
                        strTable += listSimilarityValues[i].ToString();
                    else
                        strTable += String.Format("{0}{1}", listSimilarityValues[i].ToString(), Environment.NewLine);

                }

                return OutcomeBuilder.CreateSuccess(msgSuccess, strTable).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        public Outcome FormatAsTable(ILabeledTextSimilarityValue objSimilarityValue)
        {

            string msgSuccess = "The provided LabeledTextSimilarityValue object has been successfully formatted.";
            string errFailure = "It hasn't been possible to format the provided LabeledTextSimilarityValue object.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(objSimilarityValue);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                string strTable = String.Format("{0}{1}", objSimilarityValue.ToHeader(), Environment.NewLine);
                strTable += String.Format("{0}", objSimilarityValue.ToString());

                return OutcomeBuilder.CreateSuccess(msgSuccess, strTable).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }

        // Methods (private)
        private Outcome CalculateAverage(List<double> listAverages)
        {

            /* { 0.19, 0.45 } => 0.32 */

            string msgSuccess = "The average among the provided values has been calculated.";
            string errFailure = "It hasn't been possible to calculate the average among the provided values.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(listAverages);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                double dblSum = 0.0;
                foreach (double dbl in listAverages)
                    dblSum += dbl;

                double dblAverage = dblSum / listAverages.Count;
                return OutcomeBuilder.CreateSuccess(msgSuccess, dblAverage).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        private Outcome ExtractUniqueLabels(List<LabeledTextSimilarityIndex> listSimilarityIndexes)
        {

            /*
             * 
             * Label    Average
             * sv       0.19
             * en       0.45
             * en       0.11
             * en       0.98
             * 
             *      => { "sv", "en" }
             * 
             */

            string msgSuccess = "All the unique labels in the provided list of LabeledTextSimilarityIndex objects have been extracted.";
            string errFailure = "It hasn't been possible to extract the unique labels in the provided list of LabeledTextSimilarityIndex objects.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(listSimilarityIndexes);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                HashSet<string> hshUniqueLabels = new HashSet<string>();
                foreach (LabeledTextSimilarityIndex objSimilarityIndex in listSimilarityIndexes)
                    hshUniqueLabels.Add(objSimilarityIndex.Label);

                return OutcomeBuilder.CreateSuccess(msgSuccess, hshUniqueLabels.ToList()).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        private Outcome ExtractSimilarityIndexes(string strLabel, List<LabeledTextSimilarityIndex> listSimilarityIndexes)
        {

            /*
             * 
             * Label    SimilarityIndex
             * sv       0.19
             * en       0.45
             * en       0.12
             * 
             *      => en: { 0.45, 0.12 }
             * 
             */

            string msgSuccess = "The similarity indexes in the provided list of LabeledTextSimilarityIndex objects have been extracted.";
            string errFailure = "It hasn't been possible to extract the similarity indexes in the provided list of LabeledTextSimilarityIndex objects.";
            string errNoEntries = "No entries found for the provided label in the provided list of LabeledTextSimilarityIndex objects.";

            try
            {

                Outcome objReturn = ParametersValidator.AreNullOrEmpty(
                    new object[] { strLabel, listSimilarityIndexes });
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                List<double> listIndexes = 
                    listSimilarityIndexes
                    .Where(objSimilarityIndex => objSimilarityIndex.Label == strLabel)
                    .Select(objSimilarityIndex => objSimilarityIndex.SimilarityIndex).ToList();

                objReturn = ParametersValidator.IsNullOrEmpty(listIndexes);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.CreateFailure(errNoEntries).Append(errFailure).Get();

                return OutcomeBuilder.CreateSuccess(msgSuccess, listIndexes).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }
        private Outcome GetHighestAverage(List<LabeledTextSimilarityAverage> listSimilarityAverages)
        {

            string msgSuccess = "The highest average in the provided list of LabeledTextSimilarityIndex objects have been extracted.";
            string errFailure = "It hasn't been possible to extract the highest average in the provided list of LabeledTextSimilarityIndex objects.";
            string errAllZeros = "All the provided averages are equal to zero.";
            string errAllEqual = "All the provided averages are equal each other.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(listSimilarityAverages);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                if (AreAllZerosStrategy(listSimilarityAverages))
                    return OutcomeBuilder.CreateFailure(errAllZeros).Append(errFailure).Get();

                if (!AreDistinctStrategy(listSimilarityAverages))
                    return OutcomeBuilder.CreateFailure(errAllEqual).Append(errFailure).Get();

                return OutcomeBuilder.CreateSuccess(
                    msgSuccess, 
                    GetHighestStrategy(listSimilarityAverages)).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        } 

    }

}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 23.02.2018 
 * 
 */
