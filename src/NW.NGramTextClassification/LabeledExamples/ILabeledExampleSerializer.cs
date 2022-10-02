using System;
using System.Collections.Generic;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <summary>A serializer for <see cref="LabeledExample"/>.</summary>
    public interface ILabeledExampleSerializer
    {

        /// <summary>
        /// Deserializes the provided Json string to a collection of <see cref="LabeledExample"/> objects. 
        /// <para>If <paramref name="json"/> is empty or an exception is thrown, <see cref="LabeledExampleSerializer.Default"/> will be  returned.</para>
        /// </summary>
        List<LabeledExample> DeserializeFromJsonOrDefault(string json);

        /// <summary>
        /// Serializes the provided collection of <see cref="LabeledExample"/> objects to a Json string. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        string SerializeToJson(List<LabeledExample> labeledExamples);    
    
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 02.10.2022
*/