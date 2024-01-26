﻿using System;
using NW.NGramTextClassificationClient.ApplicationSession;
using NW.NGramTextClassificationClient.Shared;

namespace NW.NGramTextClassificationClient.Application
{
    /// <summary>A factory for <see cref="ApplicationSections"/>.</summary>
    public interface IApplicationSectionsFactory
    {

        /// <summary>Creates a <see cref="ApplicationSections"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        ApplicationSections Create(ILibraryBroker libraryBroker, DependencyBag dependencyBag);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/