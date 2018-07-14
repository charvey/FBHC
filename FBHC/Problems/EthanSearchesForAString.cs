using System;

namespace FBHC.Problems
{
	class EthanSearchesForAString : Problem
	{
		protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Ethan Searches for a String", 2018, 0); }
        }

		protected override string SolveTestCase(string[] input)
		{
			throw new NotImplementedException();
		}

		protected bool Foo(string A, string B)
		{
			var i = 0;
			var j = 0;

			while (true)
			{
				if (i == A.Length) return true;
				if (j == B.Length) return false;

				if (A[i] == B[j])
				{
					i++;
					j++;
				}
				else if (i == 0)
				{
					j++;
				}
				else
				{
					i = 0;
				}
			}
		}
	}
}