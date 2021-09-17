namespace NW.NGramTextClassification.NGrams
{
    /// <summary>A contiguous sequence of N items from a given sample of text.</summary>
    public interface INGram
    {

        ushort N { get; }
        ITokenizationStrategy Strategy { get; }
        string Value { get; set; }

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/
