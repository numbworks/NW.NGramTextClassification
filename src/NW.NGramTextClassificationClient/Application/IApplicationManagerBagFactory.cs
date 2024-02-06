using System;
using NW.NGramTextClassificationClient.ApplicationSession;
using NW.NGramTextClassificationClient.Shared;

namespace NW.NGramTextClassificationClient.Application
{
    /// <summary>A factory for <see cref="ApplicationManagerBag"/>.</summary>
    public interface IApplicationManagerBagFactory
    {

        /// <summary>Creates a <see cref="ApplicationManagerBag"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        ApplicationManagerBag Create(ILibraryBroker libraryBroker, SessionManagerBag sessionManagerBag);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/