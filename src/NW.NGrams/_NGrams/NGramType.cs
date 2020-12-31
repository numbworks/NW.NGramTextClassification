using System;

namespace NW.NGrams
{
    public class NGramType<T> where T : INGram
    {

        // Fields
        // Properties
        public T Value { get; }

        // Constructors
        public NGramType(T t)
        {

            if (t == null)
                throw new ArgumentNullException(nameof(t));

            Value = t;

        }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/