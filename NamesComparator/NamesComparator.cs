using System.Collections;
using System.Collections.Generic;

namespace GlobalxCodingAssesment
{
    public class NamesComparator : IComparer<string>
    {
        private const char Separator = ' ';

        public int Compare(string x, string y)
        {
            // split both x and y into arrays of individual words. Transform array into a List so we get more methods to work with
            List<string> xComponents = new List<string>(x.Split(Separator));
            List<string> yComponents = new List<string>(y.Split(Separator));


            // extract the family name from both
            string xFamilyName = xComponents[xComponents.Count - 1];
            string yFamilyName = yComponents[yComponents.Count - 1];

            // remove last element from lists
            xComponents.RemoveAt(xComponents.Count - 1);
            yComponents.RemoveAt(yComponents.Count - 1);

            // first compare the last name
            int comparisonResult = string.Compare(xFamilyName, yFamilyName);
            if (comparisonResult < 0 || comparisonResult > 0)
            {
                // we have found that either x comes before y or vice versa
                return comparisonResult;
            }

            // in case none of the two conditions above apply, continue by comparing each given name
            for (int i = 0; i < xComponents.Count; i++)
            {
                int nameComparisonResult = string.Compare(xComponents[i], yComponents[i]);
                if (nameComparisonResult < 0 || nameComparisonResult > 0)
                {
                    return nameComparisonResult;
                }
            }

            // the two names are identical
            return 0;
        }
    }
}
