using System;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.TextSnippets
{
    /// <summary>A snippet of text.</summary>
    public class TextSnippet
    {

        #region Fields

        #endregion

        #region Properties

        public string Text { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextSnippet"/> instance using default parameters.</summary>
        /// <exception cref="ArgumentNullException"/>
        public TextSnippet(string text) 
        {

            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));

            Text = text;

        }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            return $"[ {nameof(Text)}: '{Text}' ]";

        }

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/