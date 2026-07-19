using System;
using System.Diagnostics;

namespace NW.NGramTextClassification.CLI.TerminalWindows
{
    /// <inheritdoc cref="ITerminalWindowManager"/>
    public class TerminalWindowManager : ITerminalWindowManager
    {

        #region Fields
        #endregion

        #region Properties
        public static uint CutoffWidth { get; } = 70;
        public static Func<uint?> DefaultConsoleWidthFunction { get; } = () =>
        {
            try
            {
                return (uint)Console.WindowWidth;
            }
            catch
            {
                return null;
            }
        };
        public static Func<uint?> DefaultSttySizeFunction { get; } = () =>
        {
            try
            {

                using Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/sh",
                        Arguments = "-c \"stty size | cut -d' ' -f2\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (uint.TryParse(output?.Trim(), out uint result))
                    return result;

                return null;
            }
            catch
            {
                return null;
            }
        };

        public Func<uint?> ConsoleWidthFunction { get; }
        public Func<uint?> SttySizeFunction { get; }
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TerminalWindowManager"/> instance.</summary>
        public TerminalWindowManager(Func<uint?> consoleWidthFunction = null, Func<uint?> sttySizeFunction = null)
        {
            ConsoleWidthFunction = consoleWidthFunction ?? DefaultConsoleWidthFunction;
            SttySizeFunction = sttySizeFunction ?? DefaultSttySizeFunction;
        }
        
        #endregion

        #region Methods (public)
        public uint GetOrCutoff()
        {

            uint? terminalWidth = ConsoleWidthFunction();

            if (terminalWidth == null)
                terminalWidth = SttySizeFunction();

            if (terminalWidth == null)
                terminalWidth = CutoffWidth;

            return (uint)terminalWidth;

        }
        #endregion

    }
}