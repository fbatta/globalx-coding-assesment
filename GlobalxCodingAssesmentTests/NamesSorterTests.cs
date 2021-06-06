using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlobalxCodingAssesment;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalxCodingAssesmentTests
{
    [TestClass]
    public class NamesSorterTests
    {
        ServiceProvider serviceProvider;
        IReader<IEnumerable<string>> _reader;
        IWriter<IEnumerable<string>> _writer;
        IComparer<string> _comparer;
        ISorter<IEnumerable<string>> _namesSorter;
        public NamesSorterTests()
        {
            ConfigureServices();
            // get instances of mock classes needed as dependencies
            _reader = serviceProvider.GetService<IReader<IEnumerable<string>>>();
            _writer = serviceProvider.GetService<IWriter<IEnumerable<string>>>();
            // get instance of NamesComparator
            _comparer = serviceProvider.GetService<IComparer<string>>();

            // get instance of NamesSorter
            _namesSorter = serviceProvider.GetService<ISorter<IEnumerable<string>>>();

        }

        public void ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddTransient<IReader<IEnumerable<string>>, MockReader>();
            services.AddTransient<IWriter<IEnumerable<string>>, MockWriter>();
            services.AddTransient<IComparer<string>, NamesComparator>();
            services.AddTransient<ISorter<IEnumerable<string>>, NamesSorter>();

            serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod]
        [Description("Should correctly load unsorted list from reader")]
        public void ShouldLoadUnsortedListFromReader()
        {
            // assert that unsorted list in MockReader is equal to version loaded in NamesSorter during initialisation
            CollectionAssert.AreEqual(new List<string>(_reader.Read()), new List<string>(_namesSorter.GetUnsortedList()));
        }

        [TestMethod]
        [Description("Should correctly sort a list of names first by family name, then by given name(s)")]
        public void ShouldSortList()
        {
            // do the sorting
            var sortedList = _namesSorter.Sort();

            // assert that output of sort is equal to expected output
            CollectionAssert.AreEqual(new List<string>(ExpectedSortedList.sortedList), new List<string>(sortedList));
        }

        [TestMethod]
        [Description("Should correctly write sorted list to its writer")]
        public void ShouldWriteSortedListToWriter()
        {
            // sort names
            _namesSorter.Sort();

            // write to writer
            _namesSorter.Write();

            // check that expected result and contents in writer are the same
            CollectionAssert.AreEqual(new List<string>(ExpectedSortedList.sortedList), new List<string>(MockWriterResult.result));
        }
    }
}
