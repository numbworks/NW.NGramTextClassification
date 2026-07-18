using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassification.CLI.Shared
{
    /// <summary>A factory for <see cref="SettingBag"/>.</summary>
    public interface ISettingBagFactory
    {

        /// <summary>Creates an instance of <see cref="SettingBag"/>.</summary>
        SettingBag Create();

        /// <summary>Creates an instance of <see cref="SettingBag"/> out of <paramref name="classifyData"/>.</summary>
        SettingBag Create(ClassifyData classifyData);

    }
}