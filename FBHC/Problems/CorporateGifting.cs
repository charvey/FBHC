using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FBHC.Problems
{
	class CorporateGifting : Problem
	{
		public CorporateGifting()
		{
			string cases = "";
			cases += "200000\n" + string.Join(" ", Enumerable.Range(0, 200000)) + "\n";
			cases += "200000\n" + string.Join(" ", Enumerable.Range(0, 200000).Select(i => i / 2)) + "\n";
			cases += "8\n" + "0 3 3 1 1 2 2 2" + "\n";

			File.WriteAllText("new case", cases);
		}


		private class Employee
		{
			public Employee Manager;
			public List<Employee> Subordinates = new List<Employee>();

			public int Gift
			{
				get
				{
					return Subordinates.Count == 0
						? 1
						: Enumerable.Range(1, int.MaxValue)
						.First(i => !Subordinates.Any(s => s.Gift == i));
				}
			}

			public int cost()
			{
				return Gift + Subordinates.Sum(s => s.cost());
			}
		}

		protected override ProblemConfig Config
		{
			get { return new ProblemConfig("Corporate Gifting", 2015, 1); }
		}

		protected override int LinesPerTestCase { get { return 2; } }

		protected override string SolveTestCase(string[] input)
		{
			int N = Convert.ToInt32(input[0]);
			Employee[] tree = new Employee[N];

			var ids = input[1].Split(' ').Take(N).Select(i => Convert.ToInt32(i)).ToArray();

			tree[0] = new Employee();
			for (int i = 1; i < N; i++)
			{
				Employee node = tree[i] ?? (tree[i] = new Employee());

				Employee parent = tree[ids[i] - 1] ?? (tree[ids[i] - 1] = new Employee());

				node.Manager = parent;
				parent.Subordinates.Add(node);
			}

			return tree[0].cost().ToString();
		}
	}
}