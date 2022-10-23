using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <inheritdoc cref="ITextClassifierSettingsFactory"/>
    public class TextClassifierSettingsFactory : ITextClassifierSettingsFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifierSettingsFactory"/> instance.</summary>
        public TextClassifierSettingsFactory() { }

        #endregion

        #region Methods_public

        public TextClassifierSettings Create()
            => new TextClassifierSettings();

        public TextClassifierSettings Create(ClassifyData classifyData)
        {

            TextClassifierSettings settings = new TextClassifierSettings(

                  truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                  minimumAccuracySingleLabel: classifyData.MinAccuracySingle ?? TextClassifierSettings.DefaultMinimumAccuracySingleLabel,
                  minimumAccuracyMultipleLabels: classifyData.MinAccuracyMultiple ?? TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                  folderPath: classifyData.FolderPath ?? TextClassifierSettings.DefaultFolderPath

                );

            return settings;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 23.10.2022
*/