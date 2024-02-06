using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>A factory for <see cref="ComponentBag"/>.</summary>
    public interface IComponentBagFactory
    {

        /// <summary>Creates an instance of <see cref="ComponentBag"/>.</summary>
        ComponentBag Create();

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.01.2024
*/