using FBHC.Problems;
using System;

namespace FBHC
{
	class Program
	{
		static void Main(string[] args)
		{
			var problems = new Problem[] {
				new CookingTheBooks(),
				new NewYearsResolution(),
				new LaserMaze()
			};

			foreach (var problem in problems)
			{
				try
				{
					problem.Validate();
					problem.Solve();
				}
				catch (NotImplementedException)
				{

				}
			}

			Console.In.Read();
		}
	}
}
