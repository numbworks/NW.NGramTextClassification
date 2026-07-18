namespace NW.NGramTextClassification.CLI.AsciiBanners
{
    public interface IAsciiBannerManager
    {
        string Create(string version, uint terminalWidth);
        string CreateStandard(string version);
        string CreateMini(string version);
    }
}