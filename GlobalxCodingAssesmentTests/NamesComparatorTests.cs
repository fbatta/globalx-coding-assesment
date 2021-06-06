using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlobalxCodingAssesment;

namespace GlobalxCodingAssesmentTests
{
    // run tests on the NameComparator class
    [TestClass]
    public class NamesComparatorTests
    {
        // compare various names, at least one test case for each possible outcome
        [TestMethod]
        [Description("Should correctly return which between a and b comes first")]
        [DataRow("Janet Parsons", "Vaughn Lewis", ">0")]
        [DataRow("Mikayla Lopez", "Frankie Conner Ritter", "<0")]
        [DataRow("Beau Tristan Bentley", "Beau Tristan Bentley", "=0")]
        public void ShouldSortNamesCorrectly(string a, string b, string expected)
        {
            // create instance of NamesComparator
            var comparator = new NamesComparator();

            // do the sorting
            var compareResult = comparator.Compare(a, b);

            // assert result based on expected
            if (expected == ">0")
            {
                Assert.IsTrue(compareResult > 0, $"{a} comes after {b}");
            }
            else if (expected == "<0")
            {
                Assert.IsTrue(compareResult < 0, $"{a} comes before {b}");
            }
            else if (expected == "=0")
            {
                Assert.IsTrue(compareResult == 0, $"{a} and {b} are the same");
            }
        }
    }
}
