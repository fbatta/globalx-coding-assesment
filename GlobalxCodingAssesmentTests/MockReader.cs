// class with a single static property to return the expected sorted list
using System.Collections.Generic;
using GlobalxCodingAssesment;

public class ExpectedSortedList
{
    public static IEnumerable<string> sortedList = new string[] {
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
// a class to mock an IReader: returns a predefined list of values when Read() is called
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
}
