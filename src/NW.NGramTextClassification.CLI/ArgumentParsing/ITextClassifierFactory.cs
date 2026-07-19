using System;
using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <summary>A factory for <see cref="TextClassifier"/>.</summary>
    public interface ITextClassifierFactory
    {

        /// <summary>Creates an instance of <see cref="TextClassifier"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        TextClassifier Create(ComponentBag componentBag, SettingBag settingBag);

    }
}