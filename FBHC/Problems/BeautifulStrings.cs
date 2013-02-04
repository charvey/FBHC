using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBHC.Problems
{
    class BeautifulStrings:Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Beautiful Strings", 2013, 0); }
        }

        protected override string SolveTestCase(string[] input)
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
