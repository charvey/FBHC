using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
	class Tourist : Problem
	{
		protected override ProblemConfig Config => new ProblemConfig("Tourist", 2018, 0);

		protected override int GetLinesInTestCase(IEnumerable<string> remainingLines)
		{
			return int.Parse(remainingLines.First().Split(' ')[0]) + 1;
		}

		protected override string SolveTestCase(string[] input)
		{
			var N = int.Parse(input[0].Split(' ')[0]);
			var K = int.Parse(input[0].Split(' ')[1]);
			var V = long.Parse(input[0].Split(' ')[2]);
			var A = input.Skip(1).Take(N).ToArray();

			var start = (int)(((V - 1) * K) % N);
			return string.Join(" ", A.Take((start + K) - N).Concat(A.Skip(start).Take(K)));
		}
	}
}
