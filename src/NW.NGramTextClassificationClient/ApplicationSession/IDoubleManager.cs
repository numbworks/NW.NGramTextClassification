using System;

namespace NW.NGramTextClassificationClient.ApplicationSession
{
    /// <summary>Collects all the utility methods related to <see cref="double"/> option values.</summary>
    public interface IDoubleManager
    {

        /// <summary>
        /// Checks if <paramref name="value"/> is valid or not.
        /// </summary>
        bool IsValid(string value);

        /// <summary>Parses <paramref name="value"/> or returns null if <paramref name="value"/> is null.</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        double? ParseOrDefault(string value);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 22.10.2022
*/