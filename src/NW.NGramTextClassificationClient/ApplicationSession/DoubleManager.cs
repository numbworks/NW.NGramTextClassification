namespace NW.NGramTextClassificationClient.ApplicationSession
{
    /// <inheritdoc cref="IDoubleManager"/>
    public class DoubleManager : IDoubleManager
    {

        #region Fields
        #endregion

        #region Properties
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

                return true;

            }
            catch
            {

                return false;

            }

        }
        public bool IsWithinRange(double value)
        {

            return value >= 0.0 && value <= 1.0;

        }

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 22.10.2022
*/