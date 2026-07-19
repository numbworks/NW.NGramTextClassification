using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.HelpText;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <summary>A custom implementation of help text generation.</summary>
    public class CustomHelpTextGenerator : DefaultHelpTextGenerator
    {

        #region Fields

        private readonly Func<string> _asciiBannerFunction;
        
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="CustomHelpTextGenerator"/> instance.</summary>
        public CustomHelpTextGenerator(Func<string> asciiBannerFunction)
        {
            _asciiBannerFunction = asciiBannerFunction;
        }

        #endregion

        #region Methods (public)

        /// <summary>Writes the ASCII banner before every help command to the exact same output stream the library is using.</summary>
        public override void Generate(CommandLineApplication application, TextWriter output)
        {

            string asciiBanner = _asciiBannerFunction();

            output.WriteLine();
            output.WriteLine(asciiBanner);
            output.WriteLine();

            base.Generate(application, output);

        }

        #endregion

    }

}