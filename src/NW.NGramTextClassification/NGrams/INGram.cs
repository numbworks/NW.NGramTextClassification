namespace NW.NGramTextClassification
{
    public interface INGram
    {

        ushort N { get; }
        ITokenizationStrategy Strategy { get; }
        string Value { get; set; }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/
