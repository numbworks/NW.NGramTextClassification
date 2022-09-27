using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>A factory for <see cref="TextClassifierSettings"/>.</summary>
    public interface ITextClassifierSettingsFactory
    {

        /// <summary>Creates an instance of <see cref="TextClassifierSettings"/>.</summary>
        TextClassifierSettings Create();

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/