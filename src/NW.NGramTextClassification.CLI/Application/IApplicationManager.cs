namespace NW.NGramTextClassification.CLI.Application
{
    /// <summary>Represents the CLI application.</summary>
    public interface IApplicationManager
    {

        /// <summary>Executes the CLI application according to the given arguments.</summary>
        int Execute(params string[] args);

    }
}