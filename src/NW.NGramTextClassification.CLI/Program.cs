using NW.NGramTextClassification.CLI.Application;

namespace NW.NGramTextClassification.CLI
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