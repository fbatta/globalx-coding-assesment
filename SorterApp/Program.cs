using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;

namespace GlobalxCodingAssesment
{
    class Program
    {
        // the service provider that will be built from the service collection containing the injected dependencies
        static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            // do all the configuration of services for DI
            ConfigureServices();

            // get a reference to the NameSorter service that was created as part of ConfigureServices, so we can call .Sort() on it
            NamesSorter nameSorter = serviceProvider.GetService<NamesSorter>();

            // call .Sort() on the reference to NameSorter
            IEnumerable<string> sortedList = nameSorter.Sort();

            // call .Write on NameSorter to write sorted list somewhere
            nameSorter.Write();
        }

        // create a service collection and build a service provider. We'll use automatic dependency injection to handle dependencies
        static void ConfigureServices()
        {
            // create a new ServiceCollection where we will add the servies that we want the IoC container to handle automatic injection for
            ServiceCollection services = new ServiceCollection();

            // add an IComparer<string> in the form of our NamesComparator classs
            services.AddTransient<IComparer<string>, NamesComparator>();
            // add a reader that will return an IEnumerable<string> by reading from a file
            services.AddTransient<IReader<IEnumerable<string>>, FileReader>();
            // add a write that will write an IEnumerable<string> to a file
            services.AddTransient<IWriter<IEnumerable<string>>, FileWriter>();
            // add the NamesSorter class. The previous dependencies will be automatically injected into this class that depends on all of them
            services.AddTransient<NamesSorter>();

            // expose the service provider created by building the services, so we can use it to get references to the instances created and consume them
            serviceProvider = services.BuildServiceProvider();
        }
    }
}
