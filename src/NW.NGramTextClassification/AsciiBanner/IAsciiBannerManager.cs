using System;

namespace NW.NGramTextClassification.AsciiBanner
{
    /// <summary>Collects all the helper methods related to the library's ASCII banner.</summary>
    public interface IAsciiBannerManager
    {

        /// <summary>
        /// Creates the library's ASCII banner for <paramref name="version"/>.
        /// <para>The banner template has been generated with <see href="http://www.network-science.de/ascii/">this</see> website (font: "banner3-D", width: 120).</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        string Create(string version);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/