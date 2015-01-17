using System;
using System.Linq;
using System.Collections.Generic;

namespace FBHC.Problems
{
	class Autocomplete : Problem
	{
		protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Autocomplete", 2015, 1); }
        }

		protected override int GetLinesInTestCase(IEnumerable<string> remainingLines)
		{
			return 1 + Convert.ToInt32(remainingLines.First());
		}

		protected override string SolveTestCase(string[] input)
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();

			int count = 0;
			int N = Convert.ToInt32(input[0]);
			foreach(string word in input.Skip(1).Take(N))
			{
				for(int i = 1; i <= word.Length; i++)
				{
					string prefix = word.Substring(0, i);
					if (dict.ContainsKey(prefix))
					{
						var prev = dict[prefix];
						if (prev.Length > prefix.Length)
						{
							dict[prefix] = "";
							var newPrefix = prev.Substring(0, i + 1);
							dict[newPrefix] = prev;
						}
					}

					if (!dict.ContainsKey(prefix) || i == word.Length)
					{
						dict[prefix] = word;
						count += prefix.Length;
						break;
					}
				}
			}

			return count.ToString();
		}
	}
}