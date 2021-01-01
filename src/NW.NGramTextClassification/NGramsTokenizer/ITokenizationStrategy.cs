namespace NW.NGramTextClassification
{
    public interface ITokenizationStrategy
    {

        string Pattern { get; }
        string Delimiter { get; }
        bool ToLowercase { get; }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/