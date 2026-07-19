namespace NW.NGramTextClassification.NGramTokenization
{
    /// <summary>A strategy to break a piece of text into tokens.</summary>
    public interface ITokenizationStrategy
    {

        string Pattern { get; }
        string Delimiter { get; }
        bool ToLowercase { get; }

    }
}