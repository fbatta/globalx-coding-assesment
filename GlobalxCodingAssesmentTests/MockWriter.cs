using System.Collections.Generic;
using GlobalxCodingAssesment;

namespace GlobalxCodingAssesmentTests
{
    // a class to mock the writing of an IEnumerable
    public class MockWriter : IWriter<IEnumerable<string>>
    {
        public IEnumerable<string> writtenList;
        public void Write(IEnumerable<string> thingToWrite)
        {
            MockWriterResult.result = thingToWrite;
        }
    }

    // a class containing only one static property that will be populated by MockWriter whenever Write() is called
    public class MockWriterResult
    {
        public static IEnumerable<string> result;
    }
}
