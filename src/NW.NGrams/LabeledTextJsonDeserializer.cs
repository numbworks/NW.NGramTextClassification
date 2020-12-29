using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RUBN.Shared;

namespace NW.NGrams
{
    public class LabeledTextJsonDeserializer : ILabeledTextJsonDeserializer
    {

        // Fields
        // Properties
        // Constructors
        public LabeledTextJsonDeserializer() { }

        // Methods
        public Outcome Do(string strJson)
        {

            string msgDeserialized = "The provided JSON containing labeled texts has been successfully deserialized.";

            try
            {

                List<LabeledTextJson> listLabeledTexts =
                    JsonConvert.DeserializeObject<List<LabeledTextJson>>(strJson);

                return OutcomeBuilder.CreateSuccess(msgDeserialized, listLabeledTexts).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Get();

            }

        }

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 17.02.2019
 *  Description: It represents a deserializer for LabeledTextJson objects.
 * 
 */
