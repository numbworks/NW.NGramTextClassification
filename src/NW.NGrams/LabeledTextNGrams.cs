using System;
using System.Collections.Generic;

namespace NW.NGrams
{
    public class LabeledTextNGrams
    {

        // Fields
        // Properties
        public UInt64 LabeledTextId { get; set; }
        public string Label { get; set; }
        public List<string> NGrams { get; set; }

        // Constructors
        public LabeledTextNGrams(UInt64 uintId, string strLabel, List<string> listNGrams)
        {

            LabeledTextId = uintId;
            Label = strLabel;
            NGrams = listNGrams;

        }

        // Methods

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 17.01.2018 
 *  Description: It represents a Labeled Text, in which the text itself has been converted to NGrams.
 * 
 */
