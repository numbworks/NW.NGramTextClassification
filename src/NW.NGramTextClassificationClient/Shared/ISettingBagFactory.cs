using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
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

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/