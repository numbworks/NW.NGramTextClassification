using NW.NGramTextClassificationClient.Application;

namespace NW.NGramTextClassificationClient
{
    class Program
    {

        static int Main(string[] args)
        {

            ApplicationManager applicationManager = new ApplicationManager();

            return applicationManager.Execute(args);

        }

    }

}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/