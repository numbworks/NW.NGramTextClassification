using System;
using System.Collections.Generic;

namespace NW.NGramTextClassification.Serializations
{
    /// <summary>A general purpose serializer for this library.</summary>
    public interface ISerializer<T>
    {

        /// <summary>
        /// Serializes the provided collection of objects of type T to a Json string. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        string SerializeToJson(List<T> objects);

        /// <summary>
        /// Deserializes the provided Json string to a collection of objects of type T. 
        /// <para>If <paramref name="json"/> is null/empty/invalid or an exception is thrown, <see cref="Serializer{T}.Default"/> will be returned.</para>
        /// </summary>
        List<T> DeserializeFromJsonOrDefault(string json);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/