using System.IO;

namespace NW.NGramTextClassification.Bags
{
    /// <summary>Collects all the global settings required by this library.</summary>
    public class SettingBag
    {

        #region Fields
        #endregion

        #region Properties

        public static double DefaultMinimumAccuracySingleLabel { get; } = 0.5;
        public static double DefaultMinimumAccuracyMultipleLabels { get; } = 0.0;
        public static string DefaultFolderPath { get; } = Directory.GetCurrentDirectory();
        public static bool DefaultSaveSession { get; } = false;
        public static bool DefaultCleanLabeledExamples { get; } = true;
        public static bool DefaultDisableIndexSerialization { get; } = true;
        public static bool DefaultEnableUTC { get; } = true;

        public string FolderPath { get; }
        public double? MinimumAccuracySingleLabel { get; }
        public double? MinimumAccuracyMultipleLabels { get; }
        public bool SaveSession { get; }
        public bool CleanLabeledExamples { get; }
        public bool DisableIndexSerialization { get; }
        public bool EnableUTC { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SettingBag"/> instance.</summary>
        public SettingBag(
                string folderPath = null,
                double? minimumAccuracySingleLabel = null,
                double? minimumAccuracyMultipleLabels = null,
                bool? saveSession = null,
                bool? cleanLabeledExamples = null,
                bool? disableIndexSerialization = null,
                bool? enableUTC = null)
        {

            FolderPath = folderPath ?? DefaultFolderPath;
            MinimumAccuracySingleLabel = minimumAccuracySingleLabel;
            MinimumAccuracyMultipleLabels = minimumAccuracyMultipleLabels;
            SaveSession = saveSession ?? DefaultSaveSession;
            CleanLabeledExamples = cleanLabeledExamples ?? DefaultCleanLabeledExamples;
            DisableIndexSerialization = disableIndexSerialization ?? DefaultDisableIndexSerialization;
            EnableUTC = enableUTC ?? DefaultEnableUTC;

        }

        #endregion

        #region Methods (public)
        #endregion

    }
}