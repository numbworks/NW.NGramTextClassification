using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <inheritdoc cref="ILabeledExampleSerializer"/>
    public class LabeledExampleSerializer : ILabeledExampleSerializer
    {

        #region Fields
        #endregion

        #region Properties

        public static List<LabeledExample> Default { get; } = null;
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="LabeledExampleSerializer"/> instance.</summary>
        public LabeledExampleSerializer() { }

        #endregion

        #region Methods_public

        public string SerializeToJson(List<LabeledExample> labeledExamples)
        {

            Validation.Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            string json = JsonSerializer.Serialize(labeledExamples, CreateJsonSerializerOptions());

            return json;

        }
        public List<LabeledExample> DeserializeFromJsonOrDefault(string json)
        {

            try
            {

                List<LabeledExample> labeledExamples = JsonSerializer.Deserialize<List<LabeledExample>>(json, CreateJsonSerializerOptions());

                return labeledExamples;

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
    Last Update: 02.10.2022
*/