using McMaster.Extensions.CommandLineUtils;

namespace NW.NGramTextClassificationClient.ApplicationAbout
{
    /// <summary>Represents the <c>About</c> command of the CLI application.</summary>
    public interface IAboutManager
    {

        /// <summary>Add the <c>About</c> command to the CLI application.</summary>
        CommandLineApplication Add(CommandLineApplication app);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/