using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <inheritdoc cref="ISettingCollectionFactory"/>
    public class SettingCollectionFactory : ISettingCollectionFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SettingCollectionFactory"/> instance.</summary>
        public SettingCollectionFactory() { }

        #endregion

        #region Methods_public

        public SettingCollection Create()
            => new SettingCollection();

        public SettingCollection Create(ClassifyData classifyData)
        {

            SettingCollection settingCollection = new SettingCollection(

                  truncateTextInLogMessagesAfter: SettingCollection.DefaultTruncateTextInLogMessagesAfter,
                  minimumAccuracySingleLabel: classifyData.MinAccuracySingle ?? SettingCollection.DefaultMinimumAccuracySingleLabel,
                  minimumAccuracyMultipleLabels: classifyData.MinAccuracyMultiple ?? SettingCollection.DefaultMinimumAccuracyMultipleLabels,
                  folderPath: classifyData.FolderPath ?? SettingCollection.DefaultFolderPath

                );

            return settingCollection;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/