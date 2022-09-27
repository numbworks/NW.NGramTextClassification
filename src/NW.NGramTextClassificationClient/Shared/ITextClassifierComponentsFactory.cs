using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>A factory for <see cref="TextClassifierComponents"/>.</summary>
    public interface ITextClassifierComponentsFactory
    {

        /// <summary>Creates an instance of <see cref="TextClassifierComponents"/>.</summary>
        TextClassifierComponents Create();

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/