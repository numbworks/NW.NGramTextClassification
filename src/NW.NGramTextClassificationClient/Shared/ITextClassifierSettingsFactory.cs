using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>A factory for <see cref="TextClassifierSettings"/>.</summary>
    public interface ITextClassifierSettingsFactory
    {

        /// <summary>Creates an instance of <see cref="TextClassifierSettings"/>.</summary>
        TextClassifierSettings Create();

        /// <summary>Creates an instance of <see cref="TextClassifierSettings"/> out of <paramref name="classifyData"/>.</summary>
        TextClassifierSettings Create(ClassifyData classifyData);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 23.10.2022
*/