using System;
using NW.Shared.Validation;

namespace NW.NGramTextClassification.CLI.AsciiBanners
{
    public class AsciiBannerManager : IAsciiBannerManager
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public AsciiBannerManager() { }
        #endregion

        #region Methods (public)
        public string Create(string version, uint terminalWidth)
        {

            (_, uint maxLength) = CreateFiglet();

            if (maxLength <= terminalWidth)
                return CreateStandard(version);
            else
                return CreateMini(version);
        
        }
        public string CreateStandard(string version)
        {

            Validator.ValidateStringNullOrEmpty(version, nameof(version));

            (string figlet, uint maxLength) = CreateFiglet();
            (string topLine, string bottomLine) = CreateFrame(version, maxLength);

            string asciiBanner = string.Join(Environment.NewLine, topLine, figlet, bottomLine, string.Empty);

            return asciiBanner;

        }
        public string CreateMini(string version)
        {

            /*
                ********************
                * NWNGRAM v1.0.0.0 *
                ********************
            */

            Validator.ValidateStringNullOrEmpty(version, nameof(version));

            string assemblyName = "NWNGRAM";
            string middleLine = string.Format("* {0} v{1} *", assemblyName, version);

            string topLine = new string('*', middleLine.Length);
            string bottomLine = topLine;

            return string.Join(Environment.NewLine, topLine, middleLine, bottomLine, string.Empty);    

        }
        #endregion

        #region Methods (private)
        private (string figlet, uint maxLength) CreateFiglet()
        {

            string[] lines =
            [
                "'##::: ##:'##:::::'##:'##::: ##::'######:::'########:::::'###::::'##::::'##:",
                " ###:: ##: ##:'##: ##: ###:: ##:'##... ##:: ##.... ##:::'## ##::: ###::'###:",
                " ####: ##: ##: ##: ##: ####: ##: ##:::..::: ##:::: ##::'##:. ##:: ####'####:",
                " ## ## ##: ##: ##: ##: ## ## ##: ##::'####: ########::'##:::. ##: ## ### ##:",
                " ##. ####: ##: ##: ##: ##. ####: ##::: ##:: ##.. ##::: #########: ##. #: ##:",
                " ##:. ###: ##: ##: ##: ##:. ###: ##::: ##:: ##::. ##:: ##.... ##: ##:.:: ##:",
                " ##::. ##:. ###. ###:: ##::. ##:. ######::: ##:::. ##: ##:::: ##: ##:::: ##:",
                "..::::..:::...::...:::..::::..:::......::::..:::::..::..:::::..::..:::::..::"
            ];

            string figlet = string.Join(Environment.NewLine, lines);
            uint maxLength = (uint)lines[0].Length;

            return (figlet, maxLength);
            
        }
        private (string topLine, string bottomLine) CreateFrame(string version, uint maxLength)
        {

            string versionToken = $"Version: {version}";

            int marginLength = 5;
            int totalLength = (int)maxLength - versionToken.Length - marginLength;

            string topLine = new string('*', (int)maxLength);
            string bottomLine= string.Concat(topLine.Substring(0, totalLength), versionToken, new string('*', marginLength));

            return (topLine, bottomLine);

        }
        #endregion

    }
}