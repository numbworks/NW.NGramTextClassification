namespace NW.NGramTextClassification.CLI.Shared
{
    /// <summary>Represents the CLI application.</summary>
    public interface ICLIManager
    {

        /// <summary>Executes the CLI application according to the given arguments.</summary>
        public int TryRun(string[] args);
        
    }
}