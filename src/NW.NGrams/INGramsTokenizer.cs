using RUBN.Shared;

namespace NW.NGrams
{
    public interface INGramsTokenizer
    {
        IArrayDelimiterManager ArrayDelimiterManager { get; set; }
        IArraySubsetsManager ArraySubsetsManager { get; set; }
        IParametersValidator ParametersValidator { get; set; }

        Outcome Do(ITokenizationStrategy objStrategy, string strText);
    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 19.01.2018 
 * 
 */
