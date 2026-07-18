using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <summary>A factory for <see cref="ComponentBag"/>.</summary>
    public interface IComponentBagFactory
    {

        /// <summary>Creates an instance of <see cref="ComponentBag"/>.</summary>
        ComponentBag Create();

    }
}