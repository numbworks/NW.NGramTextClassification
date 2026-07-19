namespace NW.NGramTextClassification.CLI.AsciiBanners
{
    /// <summary>Collects all the logic related to this application's ASCII banner.</summary>
    public interface IAsciiBannerManager
    {

        /// <summary>
        /// It creates a standard or a mini ASCII banner for this application out of <paramref name="version"/> and <paramref name="terminalWidth"/>.
        /// </summary>     
        string Create(string version, uint terminalWidth);

        /// <summary>
        /// It creates a standard ASCII banner for this application out of <paramref name="version"/>.
        /// </summary> 
        string CreateStandard(string version);

        /// <summary>
        /// It creates a mini ASCII banner for this application out of <paramref name="version"/>.
        /// </summary> 
        string CreateMini(string version);

    }
}