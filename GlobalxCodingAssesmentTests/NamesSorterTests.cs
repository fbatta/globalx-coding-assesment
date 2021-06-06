using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlobalxCodingAssesment;
using System.Collections.Generic;

namespace GlobalxCodingAssesmentTests
{
    // a class to mock an IReader: returns a predefined list of values when Read() is called, and also has method to return a list of the expected result after sorting
    public class MockReader : IReader<IEnumerable<string>>
    {
        public IEnumerable<string> Read()
        {
            return new string[] {
                "Janet Parsons",
                "Vaughn Lewis",
                "Adonis Julius Archer",
                "Shelby Nathan Yoder",
                "Marin Alvarez",
                "London Lindsey",
                "Beau Tristan Bentley",
                "Leo Gardner",
                "Hunter Uriah Mathew Clarke",
                "Mikayla Lopez",
                "Frankie Conner Ritter"
            };
        }

        public IEnumerable<string> GetExpectedResult()
        {
            return new string[] {
                "Adonis Julius Archer",
                "Beau Tristan Bentley",
                "Frankie Conner Ritter",
                "Hunter Uriah Mathew Clarke",
                "Janet Parsons",
                "Leo Gardner",
                "London Lindsey",
                "Marin Alvarez",
                "Mikayla Lopez",
                "Shelby Nathan Yoder",
                "Vaughn Lewis",
            };
        }
    }

    // we don't really need to implement this class since 
    public class MockWriter : IWriter<IEnumerable<string>>
    {
        public IEnumerable<string> writtenList;
        public void Write(IEnumerable<string> thingToWrite)
        {
            writtenList = thingToWrite;
        }
    }

    [TestClass]
    public class NamesSorterTests
    {
        MockReader _reader;
        MockWriter _writer;
        NamesComparator _comparer;
        NamesSorter _namesSorter;
        public NamesSorterTests()
        {
            // create instances of mock classes needed as dependencies
            _reader = new MockReader();
            _writer = new MockWriter();
            // create instance of NamesComparator
            _comparer = new NamesComparator();

            // create instance of NamesSorter
            _namesSorter = new NamesSorter(_reader, _writer, _comparer);
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
            CollectionAssert.AreEqual(new List<string>(_reader.GetExpectedResult()), new List<string>(sortedList));
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
            CollectionAssert.AreEqual(new List<string>(_reader.GetExpectedResult()), new List<string>(_writer.writtenList));
        }
    }
}
