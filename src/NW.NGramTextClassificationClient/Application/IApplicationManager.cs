namespace NW.NGramTextClassificationClient.Application
{
    /// <summary>Represents the CLI application.</summary>
    public interface IApplicationManager
    {

        /// <summary>Executes the CLI application according to the given arguments.</summary>
        int Execute(params string[] args);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.07.2022
*/