namespace NW.NGramTextClassificationClient.ApplicationSession
{
    /// <summary>Collects all the utility methods related to <see cref="double"/> option values.</summary>
    public interface IDoubleManager
    {

        /// <summary>
        /// Establishes if the <see cref="uint"/> option value is valid or not.
        /// </summary>
        bool IsValid(string value);

        /// <summary>
        /// Returns true if <paramref name="value"/> is between 0.0 and 1.0.
        /// </summary>        
        bool IsWithinRange(double value);
    
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 22.10.2022
*/