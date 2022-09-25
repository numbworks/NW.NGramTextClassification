using System;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.AsciiBanner
{
    /// <summary><inheritdoc cref="IAsciiBannerManager"/></summary>
    public class AsciiBannerManager : IAsciiBannerManager
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        ///<summary>Initializes a <see cref="AsciiBannerManager"/> instance.</summary>
        public AsciiBannerManager() { }

        #endregion

        #region Methods_public

        public string Create(string version)
        {

            Validator.ValidateStringNullOrWhiteSpace(version, nameof(version));

            (string topLine, string bottomLine) lines = CreateLines(version);

            string asciiBanner
                = string.Join(
                    Environment.NewLine,
                    lines.topLine,
                    "'##::: ##:'##:::::'##::::::'##::: ##::'######:::'########:::::'###::::'##::::'##:'########::'######::",
                    " ###:: ##: ##:'##: ##:::::: ###:: ##:'##... ##:: ##.... ##:::'## ##::: ###::'###:... ##..::'##... ##:",
                    " ####: ##: ##: ##: ##:::::: ####: ##: ##:::..::: ##:::: ##::'##:. ##:: ####'####:::: ##:::: ##:::..::",
                    " ## ## ##: ##: ##: ##:::::: ## ## ##: ##::'####: ########::'##:::. ##: ## ### ##:::: ##:::: ##:::::::",
                    " ##. ####: ##: ##: ##:::::: ##. ####: ##::: ##:: ##.. ##::: #########: ##. #: ##:::: ##:::: ##:::::::",
                    " ##::. ##:. ###. ###:: ###: ##::. ##:. ######::: ##:::. ##: ##:::: ##: ##:::: ##:::: ##::::. ######::",
                    "..::::..:::...::...:::...::..::::..:::......::::..:::::..::..:::::..::..:::::..:::::..::::::......:::",
                    lines.bottomLine,
                    string.Empty
                );

            return asciiBanner;

        }

        #endregion

        #region Methods_private

        private static (string topLine, string bottomLine) CreateLines(string version)
        {

            string versionToken = $"Version: {version}";

            int maxLength = 101;
            int marginLength = 5;

            string topLine = new string('*', maxLength);
            string bottomLine
                = topLine.Substring(0, maxLength - versionToken.Length - marginLength)
                    + versionToken
                    + new string('*', marginLength);

            return (topLine, bottomLine);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/