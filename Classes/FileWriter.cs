using System.Collections.Generic;
using System.IO;

namespace GlobalxCodingAssesment
{
    // class that writes a IEnumerable<string> to a file, in this case 'sorted-names-list.txt' in the root directory of the project
    public class FileWriter : IWriter<IEnumerable<string>>
    {
        // the file path of the file we'll be writing to
        string _filePath;
        // constructor
        public FileWriter()
        {
            // combine file path
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "sorted-names-list.txt");
        }

        // write to file
        public void Write(IEnumerable<string> thingToWrite)
        {
            File.WriteAllLines(_filePath, thingToWrite);
        }
    }
}
