using System.Linq;

namespace FBHC.Problems
{
	class BeautifulStrings:Problem
    {
		public override ProblemConfig Config => new ProblemConfig("Beautiful Strings", 2013, 0);

		public override string SolveTestCase(string[] input)
        {
            var sentence = new string(input[0].ToUpper().Where(c => 'A' <= c && c <= 'Z').ToArray());

            var letters = sentence.GroupBy(c => c).OrderByDescending(g => g.Count());

            int score = 0;
            int letterScore = 26;

            foreach (var letter in letters)
            {
                score += letterScore*letter.Count();
                letterScore--;
            }

            return ""+score;
        }
    }
}
