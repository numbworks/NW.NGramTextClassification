using System;
using System.Collections.Generic;

namespace NW.NGramTextClassification.TextSnippets
{
    /// <summary>A serializer for <see cref="TextSnippet"/>.</summary>
    public interface ITextSnippetSerializer
    {

        /// <summary>
        /// Deserializes the provided Json string to a collection of <see cref="TextSnippet"/> objects. 
        /// <para>If <paramref name="json"/> is null/empty/invalid or an exception is thrown, <see cref="TextSnippetSerializer.Default"/> will be returned.</para>
        /// </summary>
        List<TextSnippet> DeserializeFromJsonOrDefault(string json);

        /// <summary>
        /// Serializes the provided collection of <see cref="TextSnippet"/> objects to a Json string. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        string SerializeToJson(List<TextSnippet> textSnippets);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/