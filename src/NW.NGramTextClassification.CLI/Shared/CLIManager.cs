using NW.NGramTextClassification.CLI.ArgumentParsing;
using McMaster.Extensions.CommandLineUtils;

namespace NW.NGramTextClassification.CLI.Shared
{
    /// <inheritdoc cref="ICLIManager"/>
    public class CLIManager : ICLIManager
    {

        #region Fields
        #endregion

        #region Properties
        public IArgumentManager ArgumentManager { get; }
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="CLIManager"/> instance.</summary>
        public CLIManager(IArgumentManager argumentManager = null)
        {

			ArgumentManager = argumentManager ?? new ArgumentManager();

        }

        #endregion

        #region Methods (public)
        public int TryRun(string[] args)
        {

            CommandLineApplication app = ArgumentManager.Create();

            return app.Execute(args);

        }
        #endregion

        #region Methods (private)
        #endregion
    
    }
}