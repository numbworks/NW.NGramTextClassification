using System;
using System.Collections.Generic;

namespace NW.NGrams
{
    public class UniqueItemsCounter : IUniqueItemsCounter
    {

        // Fields
        // Properties
        // Constructors
        public UniqueItemsCounter() { }

        // Methods
        public uint Do(List<string> list)
        {

            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                throw new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(nameof(list)));

            return (uint)new HashSet<string>(list).Count;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/
