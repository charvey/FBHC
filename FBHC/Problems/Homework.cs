using System;
using System.Linq;

namespace FBHC.Problems
{
	class Homework : Problem
	{
		public override ProblemConfig Config => new ProblemConfig("Homework", 2015, 1);

		public override string SolveTestCase(string[] input)
		{
			int[] values = input[0].Split(' ').Select(t => Convert.ToInt32(t)).ToArray();

			int A = values[0];
			int B = values[1];
			int K = values[2];

			var primacities = getPrimacities();

			int count = 0;
			for(int i = A; i <= B; i++)
			{
				if (primacities[i] == K)
				{
					count++;
				}
			}

			return count.ToString();
		}

		private int[] _primacities;
		protected int[] getPrimacities()
		{
			if (_primacities != null)
			{
				return _primacities;
			}

			int max = (int)1e7;
			int[] x = new int[(int)1e7 + 1];
			for (int i = 2; i <= max; i++)
			{
				if (x[i] == 0)
				{
					for (int k = i; k <= max; k += i)
					{
						x[k]++;
					}
				}
			}

			return _primacities = x;
		}
	}
}