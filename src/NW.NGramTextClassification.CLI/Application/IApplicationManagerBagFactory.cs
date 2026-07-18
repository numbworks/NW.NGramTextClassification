using System;
using NW.NGramTextClassification.CLI.ApplicationSession;
using NW.NGramTextClassification.CLI.Shared;

namespace NW.NGramTextClassification.CLI.Application
{
    /// <summary>A factory for <see cref="ApplicationManagerBag"/>.</summary>
    public interface IApplicationManagerBagFactory
    {

        /// <summary>Creates a <see cref="ApplicationManagerBag"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        ApplicationManagerBag Create(ILibraryBroker libraryBroker, SessionManagerBag sessionManagerBag);

    }
}