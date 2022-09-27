using System;
using System.IO;
using NW.NGramTextClassification;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <inheritdoc cref="ILibraryBroker"/>
    public class LibraryBroker : ILibraryBroker
    {

        #region Fields

        private ITextClassifierComponentsFactory _componentsFactory { get; }
        private ITextClassifierSettingsFactory _settingsFactory { get; }
        private ITextClassifierFactory _textClassifierFactory { get; }

        #endregion

        #region Properties

        public static int Success { get; } = ((int)ExitCodes.Success);
        public static int Failure { get; } = ((int)ExitCodes.Failure);
        public static string SeparatorLine { get; } = string.Empty;
        public static Func<string, string> ErrorMessageFormatter = (message) => $"ERROR: {message}";

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="LibraryBroker"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public LibraryBroker
            (ITextClassifierComponentsFactory componentsFactory, ITextClassifierSettingsFactory settingsFactory, ITextClassifierFactory textClassifierFactory)
        {

            Validator.ValidateObject(componentsFactory, nameof(componentsFactory));
            Validator.ValidateObject(settingsFactory, nameof(settingsFactory));
            Validator.ValidateObject(textClassifierFactory, nameof(textClassifierFactory));

            _componentsFactory = componentsFactory;
            _settingsFactory = settingsFactory;
            _textClassifierFactory = textClassifierFactory;

        }

        /// <summary>Initializes a <see cref="LibraryBroker"/> instance using default parameters.</summary>
        public LibraryBroker()
            : this(new TextClassifierComponentsFactory(), new TextClassifierSettingsFactory(), new TextClassifierFactory()) { }

        #endregion

        #region Methods_public

        public int ShowHeader()
        {

            TextClassifierComponents components = _componentsFactory.Create();
            TextClassifierSettings settings = _settingsFactory.Create();
            TextClassifier textClassifier = _textClassifierFactory.Create(components, settings);

            ShowHeader(components, textClassifier);

            return Success;

        }
        public int RunAboutMain()
        {

            TextClassifierComponents components = _componentsFactory.Create();
            TextClassifierSettings settings = _settingsFactory.Create();
            TextClassifier textClassifier = _textClassifierFactory.Create(components, settings);

            ShowHeader(components, textClassifier);

            components.LoggingActionAsciiBanner(Shared.MessageCollection.Application_Description);
            components.LoggingActionAsciiBanner(SeparatorLine);

            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Author);
            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Email);
            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Url);
            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_License);

            ShowFooter(components);

            return Success;

        }

        #endregion

        #region Methods_private

        private int LogAndReturnFailure(Exception e)
        {

            TextClassifierComponents components = _componentsFactory.Create();

            components.LoggingAction(ErrorMessageFormatter(e.Message));
            if (e.InnerException != null)
                components.LoggingAction(ErrorMessageFormatter(e.InnerException.Message));

            ShowFooter(components);

            return Failure;

        }
        private void ShowHeader(TextClassifierComponents components, TextClassifier textClassifier)
        {

            components.LoggingActionAsciiBanner(SeparatorLine);
            components.LoggingActionAsciiBanner(textClassifier.AsciiBanner);
            components.LoggingActionAsciiBanner(SeparatorLine);

        }
        private void ShowFooter(TextClassifierComponents components)
        {

            components.LoggingActionAsciiBanner(SeparatorLine);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/