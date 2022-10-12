using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace NW.NGramTextClassification.TextSnippets
{
    /// <inheritdoc cref="ITextSnippetSerializer"/>
    public class TextSnippetSerializer : ITextSnippetSerializer
    {

        #region Fields
        #endregion

        #region Properties

        public static List<TextSnippet> Default { get; } = null;
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextSnippetSerializer"/> instance.</summary>
        public TextSnippetSerializer() { }

        #endregion

        #region Methods_public

        public string SerializeToJson(List<TextSnippet> textSnippets)
        {

            Validation.Validator.ValidateList(textSnippets, nameof(textSnippets));

            string json = JsonSerializer.Serialize(textSnippets, CreateJsonSerializerOptions());

            return json;

        }
        public List<TextSnippet> DeserializeFromJsonOrDefault(string json)
        {

            try
            {

                List<TextSnippet> textSnippets = JsonSerializer.Deserialize<List<TextSnippet>>(json, CreateJsonSerializerOptions());

                if (textSnippets.Count == 0)
                    return Default;

                return textSnippets;

            }
            catch
            {

                return Default;

            }

        }

        #endregion

        #region Methods_private

        private JsonSerializerOptions CreateJsonSerializerOptions()
        {

            JsonSerializerOptions options = new JsonSerializerOptions();

            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            options.WriteIndented = true;

            return options;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/