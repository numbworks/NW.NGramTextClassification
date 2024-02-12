using NW.Shared.Validation;
using System.IO;

namespace NW.NGramTextClassification.Bags
{
    /// <summary>Collects all the global settings required by this library.</summary>
    public class SettingBag
    {

        #region Fields
        #endregion

        #region Properties

        public static uint DefaultTruncateTextInLogMessagesAfter { get; } = 20;
        public static double DefaultMinimumAccuracySingleLabel { get; } = 0.5;
        public static double DefaultMinimumAccuracyMultipleLabels { get; } = 0.0;
        public static string DefaultFolderPath { get; } = Directory.GetCurrentDirectory();

        public uint TruncateTextInLogMessagesAfter { get; }
        public double MinimumAccuracySingleLabel { get; }
        public double MinimumAccuracyMultipleLabels { get; }
        public string FolderPath { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SettingBag"/> instance.</summary>
        public SettingBag(
                    uint truncateTextInLogMessagesAfter,
                    double minimumAccuracySingleLabel,
                    double minimumAccuracyMultipleLabels,
                    string folderPath)
        {

            Validator.ValidateStringNullOrWhiteSpace(folderPath, nameof(folderPath));

            TruncateTextInLogMessagesAfter = truncateTextInLogMessagesAfter;
            MinimumAccuracySingleLabel = minimumAccuracySingleLabel;
            MinimumAccuracyMultipleLabels = minimumAccuracyMultipleLabels;
            FolderPath = folderPath;

        }

        /// <summary>Initializes a <see cref="SettingBag"/> instance using default parameters.</summary>
        public SettingBag()
            : this(
                  truncateTextInLogMessagesAfter: DefaultTruncateTextInLogMessagesAfter,
                  minimumAccuracySingleLabel: DefaultMinimumAccuracySingleLabel,
                  minimumAccuracyMultipleLabels: DefaultMinimumAccuracyMultipleLabels,
                  folderPath: DefaultFolderPath
                  )
        { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/