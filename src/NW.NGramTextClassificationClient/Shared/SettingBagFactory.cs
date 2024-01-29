using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <inheritdoc cref="ISettingBagFactory"/>
    public class SettingBagFactory : ISettingBagFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SettingBagFactory"/> instance.</summary>
        public SettingBagFactory() { }

        #endregion

        #region Methods_public

        public SettingBag Create()
            => new SettingBag();

        public SettingBag Create(ClassifyData classifyData)
        {

            SettingBag settingBag = new SettingBag(

                  truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                  minimumAccuracySingleLabel: classifyData.MinAccuracySingle ?? SettingBag.DefaultMinimumAccuracySingleLabel,
                  minimumAccuracyMultipleLabels: classifyData.MinAccuracyMultiple ?? SettingBag.DefaultMinimumAccuracyMultipleLabels,
                  folderPath: classifyData.FolderPath ?? SettingBag.DefaultFolderPath

                );

            return settingBag;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/