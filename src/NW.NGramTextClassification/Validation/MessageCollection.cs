using System;
using System.Collections.Generic;
using System.Linq;
using NW.Shared.Files;

namespace NW.NGramTextClassification.Validation
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="Validation"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static Func<string, string, string> FirstValueIsGreaterOrEqualThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater or equal than '{variableName2}''s value.";
        public static Func<string, string, string> FirstValueIsGreaterThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater than '{variableName2}''s value.";
        public static Func<string, string> VariableContainsZeroItems
            = (variableName) => $"'{variableName}' contains zero items.";
        public static Func<string, string> VariableCantBeLessThanOne
            = (variableName) => $"'{variableName}' can't be less than one.";
        public static Func<string, string, string> DividingMustReturnWholeNumber
            = (variableName1, variableName2) => $"Dividing '{variableName1}' by '{variableName2}' must return a whole number.";
        public static Func<Dictionary<string, int>, string> AtLeastOneSubScraper =
                (subscrapers)
                    => {

                        /*
                            At least one sub-scraper didn't return the expected amount of results 
                            ('urls':'20','titles':'20','createDates':'20','applicationDates':'20','workAreas':'20',
                            'workAreasWithoutZones':'20','workingHours':'20','jobTypes':'20','jobIds':'20').
                        */

                        List<string> results = subscrapers.Select(item => $"'{item.Key}':'{item.Value}'").ToList();
                        string joined = string.Join(",", results);

                        return string.Concat(
                                "At least one sub-scraper didn't return the expected amount of results (",
                                joined,
                                ")."
                                );

                    };
        public static Func<string, string, string> FirstDateIsOlderOrEqual
            = (variableName1, variableName2) => $"'{variableName1}''s is older or equal than '{variableName2}'.";
        public static Func<IFileInfoAdapter, string> ProvidedPathDoesntExist
            = (file) => $"The provided path doesn't exist: '{file.FullName}'.";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 29.06.2022
*/