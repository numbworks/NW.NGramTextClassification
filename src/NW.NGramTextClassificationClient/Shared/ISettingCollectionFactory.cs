using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>A factory for <see cref="SettingCollection"/>.</summary>
    public interface ISettingCollectionFactory
    {

        /// <summary>Creates an instance of <see cref="SettingCollection"/>.</summary>
        SettingCollection Create();

        /// <summary>Creates an instance of <see cref="SettingCollection"/> out of <paramref name="classifyData"/>.</summary>
        SettingCollection Create(ClassifyData classifyData);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/