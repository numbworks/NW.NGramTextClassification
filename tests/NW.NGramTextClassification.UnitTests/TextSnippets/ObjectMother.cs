using System;
using System.Collections.Generic;
using NW.NGramTextClassification.TextSnippets;

namespace NW.NGramTextClassification.UnitTests.TextSnippets
{
    public static class ObjectMother
    {

        #region Properties

        public static List<TextSnippet> TextSnippets = new List<TextSnippet>()
        {

            new TextSnippet(text: "We are looking for several skilled and driven developers to join our team."),
            new TextSnippet(text: "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.")

        };
        public static string TextSnippetsAsJson_Content = Properties.Resources.TextSnippetsAsJson;

        #endregion

        #region Methods

        public static bool AreEqual(TextSnippet obj1, TextSnippet obj2)
        {

            return string.Equals(obj1.Text, obj2.Text, StringComparison.InvariantCulture);

        }
        public static bool AreEqual(List<TextSnippet> list1, List<TextSnippet> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/