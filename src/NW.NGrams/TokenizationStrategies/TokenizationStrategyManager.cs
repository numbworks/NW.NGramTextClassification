using System.Collections.Generic;

namespace NW.NGrams
{
    public class TokenizationStrategyManager : ITokenizationStrategyManager
    {

        // Fields
        // Properties
        // Constructors	
        public TokenizationStrategyManager() { }

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
    Last Update: 29.12.2020

*/
