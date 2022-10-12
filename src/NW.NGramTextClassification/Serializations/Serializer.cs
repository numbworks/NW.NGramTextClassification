using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace NW.NGramTextClassification.Serializations
{
    /// <inheritdoc cref="ISerializer{T}"/>
    public class Serializer<T>
    {

        #region Fields

        #endregion

        #region Properties

        public static List<T> Default { get; } = null;

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="Serializer{T}"/> instance using default parameters.</summary>	
        public Serializer() { }

        #endregion

        #region Methods_public

        public string SerializeToJson(List<T> objects)
        {

            Validation.Validator.ValidateList(objects, nameof(objects));

            string json = JsonSerializer.Serialize(objects, CreateJsonSerializerOptions());

            return json;

        }
        public List<T> DeserializeFromJsonOrDefault(string json)
        {

            try
            {

                List<T> objects = JsonSerializer.Deserialize<List<T>>(json, CreateJsonSerializerOptions());

                if (objects.Count == 0)
                    return Default;

                return objects;

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