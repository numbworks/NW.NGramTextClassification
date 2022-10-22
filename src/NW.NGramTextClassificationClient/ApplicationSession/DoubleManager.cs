using System.Linq;

namespace NW.NGramTextClassificationClient.ApplicationSession
{
    /// <inheritdoc cref="IDoubleManager"/>
    public class DoubleManager : IDoubleManager
    {

        #region Fields
        #endregion

        #region Properties

        public static double MininumValue { get; } = 0.0;
        public static double MaximumValue { get; } = 1.0;

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="DoubleManager"/> instance.</summary>	
        public DoubleManager() { }

        #endregion

        #region Methods_public

        public bool IsValid(string value)
        {

            try
            {
                double parsed = double.Parse(value);

                return parsed >= 0.0 && parsed <= 1.0;

            }
            catch
            {

                return false;

            }

        }
        public double Parse(string value)
            => double.Parse(value);

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 22.10.2022
*/