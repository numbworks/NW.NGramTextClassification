using System;
using System.IO;
using NW.NGramTextClassification.Filenames;

namespace NW.NGramTextClassification.UnitTests.Filenames
{
    public static class ObjectMother
    {

        #region Proprietes

        public static string FakeFilePath = "Folder" + Path.DirectorySeparatorChar + "SubFolder" + Path.DirectorySeparatorChar;

        public static DateTime FakeNow = new DateTime(2021, 05, 01);
        public static string FakeNowString = FakeNow.ToString(FilenameFactory.DefaultFormatNow);

        public static string Filename_TextSnippetsJson
            = string.Concat(
                        FakeFilePath,
                        FilenameFactory.DefaultMainToken,
                        "_",
                        FilenameFactory.DefaultTextSnippetsToken,
                        "_",
                        FakeNowString,
                        ".",
                        FilenameFactory.DefaultJsonExtension
                        );

        public static string Filename_LabeledExamplesJson
            = string.Concat(
                        FakeFilePath,
                        FilenameFactory.DefaultMainToken,
                        "_",
                        FilenameFactory.DefaultLabeledExamplesToken,
                        "_",
                        FakeNowString,
                        ".",
                        FilenameFactory.DefaultJsonExtension
                        );

        public static string Filename_SessionJson
            = string.Concat(
                        FakeFilePath,
                        FilenameFactory.DefaultMainToken,
                        "_",
                        FilenameFactory.DefaultSessionToken,
                        "_",
                        FakeNowString,
                        ".",
                        FilenameFactory.DefaultJsonExtension
                        );

        #endregion

    }
}