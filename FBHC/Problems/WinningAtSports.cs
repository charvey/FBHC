using System;
using System.Linq;

namespace FBHC.Problems
{
	class WinningAtSports : Problem
	{
		protected override ProblemConfig Config
		{
			get { return new ProblemConfig("Winning at Sports", 2015, 1); }
		}

		const int MOD = 1000000007;

		protected override string SolveTestCase(string[] input)
		{
			int[] values = input[0].Split('-').Select(t => Convert.ToInt32(t)).ToArray();

			int W = values[0];
			int L = values[1];

			return stressfree(W, L) + " " + stressfull(W, L);
		}

		private int stressfree(int w, int l, int[][] sfree = null)
		{
			if (sfree == null)
			{
				sfree = new int[w + 1][];
			}

			if (sfree[w] == null)
			{
				sfree[w] = new int[l + 1];
			}

			if (sfree[w][l] == 0)
			{
				if (l == 0)
					sfree[w][l] = 1;
				else if (w == l + 1)
					sfree[w][l] = stressfree(w, l - 1, sfree);
				else
					sfree[w][l] = stressfree(w - 1, l, sfree) + stressfree(w, l - 1, sfree);
			}

			sfree[w][l] %= MOD;

			return sfree[w][l];
		}

		private int stressfull(int w, int l, int[][] sfull = null)
		{
			if (sfull == null)
			{
				sfull = new int[w + 1][];
			}

			if (sfull[w] == null)
			{
				sfull[w] = new int[l + 1];
			}

			if (sfull[w][l] == 0)
			{
				if (w == 0)
					sfull[w][l] = 1;
				else if (w >= l)
					sfull[w][l] = stressfull(w - 1, l, sfull);
				else
					sfull[w][l] = stressfull(w - 1, l, sfull) + stressfull(w, l - 1, sfull);
			}

			sfull[w][l] %= MOD;

			return sfull[w][l];
		}
	}
}