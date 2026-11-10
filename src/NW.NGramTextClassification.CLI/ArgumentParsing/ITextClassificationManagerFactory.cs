namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <summary>A factory for <see cref="TextClassificationManagerFactory"/>.</summary>
    public interface ITextClassificationManagerFactory
    {

        /// <summary>Creates an instance of <see cref="TextClassificationManagerFactory"/>.</summary>
        public ITextClassificationManager Create(bool enableUTC = false);

    }
}