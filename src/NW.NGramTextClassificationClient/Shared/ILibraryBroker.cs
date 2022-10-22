using NW.NGramTextClassification;
using NW.NGramTextClassificationClient.Application;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <summary>The middleware between <see cref="ApplicationManager"/> and <see cref="TextClassifier"/>.</summary>
    public interface ILibraryBroker
    {

        /// <summary>Shows the application's ASCII banner.</summary>
        /// <returns>Always <see cref="ExitCodes.Success"/></returns>
        int ShowHeader();

        /// <summary>Runs the <c>about</c> command of the CLI application.</summary>
        /// <returns>Always <see cref="ExitCodes.Success"/></returns>
        int RunAboutMain();

        /// <summary>Runs the <c>classify</c> sub-command of the CLI application.</summary>
        /// <returns><see cref="ExitCodes"/></returns>
        int RunSessionClassify(ClassifyData classifyData);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 22.10.2022
*/