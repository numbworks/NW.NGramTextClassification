using System;

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

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/