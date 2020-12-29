using System;

namespace NW.NGrams
{
    public class LabeledTextJson
    {

        // Fields
        // Properties
        public UInt64 LabeledTextId { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }

        // Constructors
        public LabeledTextJson() { }

        // Methods
        public override string ToString()
        {

            return String.Concat(
                "{ \"",
                 nameof(LabeledTextId),
                 "\": \"",
                 LabeledTextId.ToString(),
                 "\", \"",
                 nameof(Label),
                 "\": \"",
                 Label,
                 "\", \"",
                 nameof(Text),
                 "\": \"",
                 CutText(Text),
                 "\" }"); 

        }
        private string CutText(string strText)
        {

            if (String.IsNullOrEmpty(strText)) return strText;
            if (strText.Length < 10) return strText;

            return Text.Substring(0, 10) + " [...]";

        }

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 15.01.2018 
 *  Description: It defines a JSON containing a labeled text.
 * 
 */
