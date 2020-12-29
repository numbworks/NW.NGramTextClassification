using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using RUBN.Shared;

namespace NW.NGrams
{
    public class NGramsTokenizer : INGramsTokenizer
    {

        // Fields
        // Properties
        public IArraySubsetsManager ArraySubsetsManager { get; set; } = new ArraySubsetsManager();
        public IArrayDelimiterManager ArrayDelimiterManager { get; set; } = new ArrayDelimiterManager();
        public IParametersValidator ParametersValidator { get; set; } = new ParametersValidator();

        // Constructors
        public NGramsTokenizer() { }

        // Methods
        public Outcome Do(ITokenizationStrategy objStrategy, string strText)
        {

            string msgSuccess = "The provided text has been successfully tokenized.";
            string errFailure = "It hasn't been possible to tokenize the provided text.";
            string errAtLeastOne = "'{0}' must be at least equal to 1 (actual value:'{1}').";
            string errNoMatches = "No matches found in the provided text for the provided pattern: '{0}'.";

            try
            {

                Outcome objReturn = ParametersValidator.AreNullOrEmpty(
                    new object[] { objStrategy.Pattern, strText });
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                if (objStrategy.N < 1)
                    return OutcomeBuilder.CreateFailure(
                        String.Format(errAtLeastOne, nameof(objStrategy.N), objStrategy.N.ToString()))
                        .Append(errFailure).Get();

                // "This is a sample text." => "This", "is", ..., "text"
                MatchCollection objMatches = Regex.Matches(strText, objStrategy.Pattern);
                if (objMatches.Count == 0)
                    return OutcomeBuilder.CreateFailure(
                        String.Format(errNoMatches, objStrategy.Pattern))
                        .Append(errFailure).Get();

                // ["This", "is", ..., "text"]
                string[] arrWords = new string[objMatches.Count];
                for (int i = 0; i < objMatches.Count; i++)
                    arrWords[i] = objMatches[i].Value;

                List<string> listNGrams = new List<string>();
                for (int i = 0; i < arrWords.Length; i++)
                {

                    // The last x NGrams are shorter in length...
                    int intDynamicN = objStrategy.N;
                    if ((arrWords.Length - i) < objStrategy.N)
                        intDynamicN = (arrWords.Length - i);

                    // For N = 3: ["This", "is", "a"], ["is", "a", "sample"], ...
                    objReturn = ArraySubsetsManager.GetSubset(arrWords, i, intDynamicN);
                    /* if (objReturn.IsFailure()) ... never happens due of (objMatches.Count == 0) and intDynamicN */
                    string[] arrNGram = (string[])objReturn.Result;

                    // [ "This", "is", "a" ] => [ "This", " ", "is", " ", "a" ]
                    objReturn = ArrayDelimiterManager.AddDelimiter(arrNGram, objStrategy.Delimiter);
                    /* if (objReturn.IsFailure()) ... never happens due of (objMatches.Count == 0) */
                    arrNGram = (string[])objReturn.Result;

                    // [ "This", " ", "is", " ", "a" ] => "This is a"
                    StringBuilder objNGram = new StringBuilder();
                    foreach (string strWord in arrNGram)
                        objNGram.Append(strWord);

                    // "This is a" => "this is a" or "This is a"
                    if (objStrategy.ConvertAllToLowercase)
                        listNGrams.Add(objNGram.ToString().ToLower());
                    else
                        listNGrams.Add(objNGram.ToString());

                }

                return OutcomeBuilder.CreateSuccess(msgSuccess, listNGrams).Get();

            }
            catch(Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 02.02.2018 
 *  Description: It represents the process to tokenize a text in ngrams of a given size.
 * 
 */
