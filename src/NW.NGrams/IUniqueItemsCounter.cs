using System.Collections.Generic;
using RUBN.Shared;

namespace NW.NGrams
{
    public interface IUniqueItemsCounter
    {

        IParametersValidator ParametersValidator { get; set; }
        Outcome Do(List<string> list);

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 02.02.2018
 * 
 */
