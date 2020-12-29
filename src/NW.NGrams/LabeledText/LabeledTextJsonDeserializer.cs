using System.Collections.Generic;
using Newtonsoft.Json;

namespace NW.NGrams
{
    public class LabeledTextJsonDeserializer : ILabeledTextJsonDeserializer
    {

        // Fields
        // Properties
        // Constructors
        public LabeledTextJsonDeserializer() { }

        // Methods
        public List<LabeledTextJson> Do(string json)
            => JsonConvert.DeserializeObject<List<LabeledTextJson>>(json);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/