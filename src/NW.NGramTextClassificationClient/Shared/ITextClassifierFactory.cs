using System;
using NW.NGramTextClassification;
using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>A factory for <see cref="TextClassifier"/>.</summary>
    public interface ITextClassifierFactory
    {

        /// <summary>Creates an instance of <see cref="TextClassifier"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        TextClassifier Create(ComponentBag componentBag, SettingBag settingBag);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/