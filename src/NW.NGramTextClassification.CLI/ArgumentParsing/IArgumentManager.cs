using McMaster.Extensions.CommandLineUtils;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <summary>Represents an argument manager.</summary>
    public interface IArgumentManager
    {

        /// <summary>Creates the argument manager.</summary>
        public CommandLineApplication Create();
    
    }
}