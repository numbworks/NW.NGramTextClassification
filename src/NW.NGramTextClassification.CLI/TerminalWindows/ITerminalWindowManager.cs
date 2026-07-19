namespace NW.NGramTextClassification.CLI.TerminalWindows
{
    /// <summary>Collects all the logic related to the management of the terminal window.</summary>
    public interface ITerminalWindowManager
    {

        /// <summary>
        /// Returns the terminal width or the cutoff value.
        /// </summary>
        public uint GetOrCutoff();

    }
}