using System;
using FBHC.Problems;

namespace FBHC
{
	class Program
	{
		static void Main(string[] args)
		{
			var newProblems = new ProblemConfig[]
			{
				new ProblemConfig("New Year's Resolution",2015,0)
			};

			var creator = new ProblemCreator();
			foreach(var config in newProblems)
			{
				creator.Create(config);
			}

			var problems = new Problem[] {
				new CookingTheBooks(),
				new NewYearsResolution()
			};

			foreach (var problem in problems)
			{
				try
				{
					problem.Validate();
					//problem.Solve();
				}
				catch (NotImplementedException)
				{

				}
			}

			Console.In.Read();
		}
	}
}
