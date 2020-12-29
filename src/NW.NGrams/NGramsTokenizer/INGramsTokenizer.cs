using System.Collections.Generic;

namespace NW.NGrams
{
    public interface INGramsTokenizer
    {
        List<string> Do(ITokenizationStrategy objStrategy, string strText);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/