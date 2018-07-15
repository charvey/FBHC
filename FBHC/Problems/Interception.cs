using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
	class Interception : Problem
	{
		public override ProblemConfig Config => new ProblemConfig("Interception", 2018, 0);

		protected override int GetLinesInTestCase(IEnumerable<string> remainingLines)
		{
			return int.Parse(remainingLines.First()) + 1;
		}

		public override string SolveTestCase(string[] input)
		{
			var N = int.Parse(input[0]);
			var P = input.Skip(1).Take(N).Select(int.Parse).ToArray();

			if (N % 2 == 0)
				return $"{0}";
			else
				return $"{1}\n{0.0}";
		}
	}
}