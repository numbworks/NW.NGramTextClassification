using System;
using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <summary>A factory for <see cref="TextClassificationManagerFactory"/>.</summary>
    public interface ITextClassificationManagerFactory
    {

        /// <summary>The VersioFunction used by <see cref="Create"/>.</summary>
        public static Func<string> DefaultVersionFunction { get; }

        /// <summary>The DefaultErrorFormattingFunction used by <see cref="Create"/>.</summary>
        public static Func<string, string> DefaultErrorFormattingFunction { get; }

        /// <summary>Creates an instance of <see cref="TextClassificationManagerFactory"/>.</summary>
        public ITextClassificationManager Create(SettingBag settingBag, out Action<string> loggingAction);

    }
}