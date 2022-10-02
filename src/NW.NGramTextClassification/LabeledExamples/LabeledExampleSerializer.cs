using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <inheritdoc cref="ILabeledExampleSerializer"/>
    public class LabeledExampleSerializer : ILabeledExampleSerializer
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="LabeledExampleSerializer"/> instance.</summary>
        public LabeledExampleSerializer() { }

        #endregion

        #region Methods_public

        public string SerializeToJson(List<LabeledExample> labeledExamples)
        {

            Validation.Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            string json = "";

            return json;

        }
        public List<LabeledExample> DeserializeFromJson(string json)
        {

            return null;

        }

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 02.10.2022
*/