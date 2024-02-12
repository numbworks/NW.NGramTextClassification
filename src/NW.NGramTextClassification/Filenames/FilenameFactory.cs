using System;
using System.IO;
using NW.Shared.Validation;

namespace NW.NGramTextClassification.Filenames
{
    /// <inheritdoc cref="IFilenameFactory"/>
    public class FilenameFactory : IFilenameFactory
    {

        #region Fields
        #endregion

        #region Properties

        public static string DefaultFileNameTemplate { get; } = "{0}_{1}_{2}.{3}";
        public static string DefaultMainToken { get; } = "ngramtc";
        public static string DefaultTextSnippetsToken { get; } = "textsnippets";
        public static string DefaultLabeledExamplesToken { get; } = "labeledexamples";
        public static string DefaultSessionToken { get; } = "session";
        public static string DefaultFormatNow { get; } = "yyyyMMddHHmmssfff";
        public static string DefaultJsonExtension { get; } = "json";

        #endregion

        #region Constructors

        ///<summary>Initializes a <see cref="FilenameFactory"/> instance.</summary>
        public FilenameFactory() { }

        #endregion

        #region Methods_public

        public string CreateForTextSnippetsJson(string folderPath, DateTime now)
            => ValidateAndCreate(folderPath, DefaultMainToken, DefaultTextSnippetsToken, now, DefaultJsonExtension);
        public string CreateForLabeledExamplesJson(string folderPath, DateTime now)
            => ValidateAndCreate(folderPath, DefaultMainToken, DefaultLabeledExamplesToken, now, DefaultJsonExtension);
        public string CreateForSessionJson(string folderPath, DateTime now)
            => ValidateAndCreate(folderPath, DefaultMainToken, DefaultSessionToken, now, DefaultJsonExtension);

        #endregion

        #region Methods_private

        private string ValidateAndCreate(string folderPath, string mainToken, string secondaryToken, DateTime now, string extension)
        {

            Validator.ValidateStringNullOrWhiteSpace(folderPath, nameof(folderPath));

            string template = DefaultFileNameTemplate;
            string nowstring = now.ToString(DefaultFormatNow);

            string fileName = string.Format(template, mainToken, secondaryToken, nowstring, extension);

            return Path.Combine(folderPath, fileName);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.10.2022
*/