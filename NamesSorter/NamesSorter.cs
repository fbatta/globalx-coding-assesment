using System;
using System.Collections.Generic;

namespace GlobalxCodingAssesment
{
    // a class that reads a list from somewhere, sorts it by some criteria and writes the resulting sorted list to somewhere
    public class NamesSorter : ISorter<IEnumerable<string>>
    {
        // the comparer used to do the sorting
        IComparer<string> _comparer;
        // the thing that will do the reading and will get us an IEnumerable<string>
        IReader<IEnumerable<string>> _reader;
        // the thing that will write our sorted IEnumerable<string> to somewhere
        IWriter<IEnumerable<string>> _writer;
        // the unsorted list
        List<string> _unsortedList;
        // the sorted list
        List<string> _sortedList;

        // Default constructor: takes an IReader, IWriter and IComparer
        public NamesSorter(IReader<IEnumerable<string>> reader, IWriter<IEnumerable<string>> writer, IComparer<string> comparer)
        {
            _reader = reader;
            _writer = writer;
            _comparer = comparer;
            // read from reader and create a list
            Read();
        }

        // read from reader and create a list
        public void Read()
        {
            _unsortedList = new List<string>(_reader.Read());
        }

        // Perform sorting on the array and return the sorted version
        public IEnumerable<string> Sort()
        {
            // clone and sort the list
            _sortedList = new List<string>(_unsortedList);
            _sortedList.Sort();
            // return the sorted List
            return _sortedList;
        }

        // write sorted list somewhere
        public void Write()
        {
            // throw error sorted list is empty
            if (_sortedList.Count == 0)
            {
                throw new InvalidOperationException("Sorted array is empty");
            }
            // write our sorted list to somewhere
            _writer.Write(_sortedList);
        }
    }
}
