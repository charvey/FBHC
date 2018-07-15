using System;

namespace FBHC
{
	class Program
	{
		static void Main(string[] args)
		{
			var problems = new Problem[] {
				//new EthanSearchesForAString()
			};

			foreach (var problem in problems)
			{
				try
				{
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
