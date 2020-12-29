using System.Collections.Generic;

namespace NW.NGrams
{
    public class DefaultTokenizationStrategies : ITokenizationStrategies
    {

        // Fields
        // Properties
        // Constructors	
        public DefaultTokenizationStrategies() { }

        // Methods
        public List<ITokenizationStrategy> Get()
            => new List<ITokenizationStrategy>() {
                    new TokenizationStrategyMonograms(),
                    new TokenizationStrategyBigrams(),
                    new TokenizationStrategyTrigrams() };

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 23.08.2018

*/
