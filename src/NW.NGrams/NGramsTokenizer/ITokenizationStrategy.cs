namespace NW.NGrams
{
    public interface ITokenizationStrategy
    {
        bool ConvertAllToLowercase { get; }
        string Delimiter { get; }
        ushort N { get; }
        string Pattern { get; }
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/
