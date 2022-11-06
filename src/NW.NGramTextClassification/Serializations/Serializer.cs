using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace NW.NGramTextClassification.Serializations
{
    /// <inheritdoc cref="ISerializer{T}"/>
    public class Serializer<T> : ISerializer<T>
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

        public string Serialize(T obj)
        {

            Validation.Validator.ValidateObject(obj, nameof(obj));

            string json = JsonSerializer.Serialize(obj, CreateJsonSerializerOptions());

            return json;

        }
        public string Serialize(T obj, Func<T, dynamic> modifier)
        {

            Validation.Validator.ValidateObject(obj, nameof(obj));
            Validation.Validator.ValidateObject(modifier, nameof(modifier));

            dynamic modified = modifier(obj);
            string json = JsonSerializer.Serialize(modified, CreateJsonSerializerOptions());

            return json;

        }
        public string Serialize(List<T> objects)
        {

            Validation.Validator.ValidateList(objects, nameof(objects));

            string json = JsonSerializer.Serialize(objects, CreateJsonSerializerOptions());

            return json;

        }
        
        public List<T> DeserializeManyOrDefault(string json)
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
        public T DeserializeOrDefault(string json)
        {

            try
            {

                T obj = JsonSerializer.Deserialize<T>(json, CreateJsonSerializerOptions());

                return obj;

            }
            catch(Exception e)
            {

                return default(T);

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
    Last Update: 06.11.2022
*/