using System;

namespace FBHC.Problems
{
	public class Sample : Problem
	{
		protected override ProblemConfig Config
		{
			get { return new ProblemConfig("Sample", 2011, 0); }
		}

		protected override string SolveTestCase(string[] input)
		{
			throw new NotImplementedException();
		}
	}
}
