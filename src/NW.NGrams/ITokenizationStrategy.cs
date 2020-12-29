namespace NW.NGrams
{
    public interface ITokenizationStrategy
    {
        bool ConvertAllToLowercase { get; set; }
        string Delimiter { get; set; }
        ushort N { get; set; }
        string Pattern { get; set; }
    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 29.12.2017 
 * 
 */
