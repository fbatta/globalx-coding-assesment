using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GlobalxCodingAssesment
{
    // a class that implements IReader and returns an IEnumerable<string> from its read() method
    public class FileReader : IReader<IEnumerable<string>>
    {
        // the path of the file where we get the data from
        string _filePath { get; }
        // default constructor
        public FileReader()
        {
            // get the path of the current directory
            string workingDirectory = Directory.GetCurrentDirectory();
            // combine workingDirectory with the name of the file we want to get
            _filePath = Path.Combine(workingDirectory, "unsorted-names-list.txt");
        }
        public IEnumerable<string> Read()
        {
            // File.ReadAllLines returns an array of strings
            string[] fileLines = File.ReadAllLines(_filePath);
            // get the enumerator from the string array. Do some type casting to get the generic version of IEnumerable
            return fileLines;
        }
    }
}
