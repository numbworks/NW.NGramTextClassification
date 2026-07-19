using System;
using NW.NGramTextClassification.CLI.ArgumentParsing;

namespace NW.NGramTextClassification.CLI.Messages
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="CLI"/>.</summary>
    public static class MessageCollection
    {

        public static Func<string, string> LoadingFileNameReturnedDefault =
            (fileName) => $"Loading the content of '{fileName}' returned a default value. Please check the content of the file, it may be null or invalid.";
        public static Func<string, string, string> ValueIsInvalidOrNotWithinRange
            = (name, value) => $"{name} ('{value}') is invalid or not within the expected range ('{nameof(DoubleManager.MininumValue)}':'{DoubleManager.MininumValue}', '{nameof(DoubleManager.MaximumValue)}':'{DoubleManager.MaximumValue}').";

        public static string PressAButtonToCloseTheWindow = "Press a button to close the window.";

    }
}