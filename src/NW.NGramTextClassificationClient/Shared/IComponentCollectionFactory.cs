using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>A factory for <see cref="ComponentCollection"/>.</summary>
    public interface IComponentCollectionFactory
    {

        /// <summary>Creates an instance of <see cref="ComponentCollection"/>.</summary>
        ComponentCollection Create();

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.01.2024
*/