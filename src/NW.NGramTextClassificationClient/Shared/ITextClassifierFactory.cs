using System;
using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>A factory for <see cref="TextClassifier"/>.</summary>
    public interface ITextClassifierFactory
    {

        /// <summary>Creates an instance of <see cref="TextClassifier"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        TextClassifier Create(ComponentCollection componentCollection, TextClassifierSettings settings);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.01.2024
*/