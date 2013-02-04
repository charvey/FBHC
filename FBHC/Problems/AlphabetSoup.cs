using System.Linq;

namespace FBHC.Problems
{
    public class AlphabetSoup : Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Alphabet Soup", 2012, 0); }
        }

        protected override string SolveTestCase(string[] input)
        {
            var letters = input[0].Where(c => "HACKERCUP".Contains(c));

            return "HACKERCUP".Min(c => letters.Count(l => c == l) / (c == 'C' ? 2 : 1)).ToString();
        }
    }
}
