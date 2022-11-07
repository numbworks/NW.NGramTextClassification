using System;
using System.Collections.Generic;

namespace NW.NGramTextClassification.Serializations
{
    /// <summary>A general purpose serializer for this library.</summary>
    public interface ISerializer<T>
    {

        /// <summary>
        /// Serializes the provided object of type T to a Json string. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        string Serialize(T obj);

        /// <summary>
        /// Serializes the provided collection of objects of type T to a Json string. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        string Serialize(List<T> objects);

        /// <summary>
        /// Deserializes the provided Json string to a collection of objects of type T. 
        /// <para>If <paramref name="json"/> is null/empty/invalid or an exception is thrown, <see cref="Serializer{T}.Default"/> will be returned.</para>
        /// </summary>
        List<T> DeserializeManyOrDefault(string json);

        /// <summary>
        /// Deserializes the provided Json string to an object of type T. 
        /// <para>If <paramref name="json"/> is null/empty/invalid or an exception is thrown, default of T will be returned.</para>
        /// </summary>
        T DeserializeOrDefault(string json);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 06.11.2022
*/