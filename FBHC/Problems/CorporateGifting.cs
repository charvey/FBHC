using System;
using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
	class CorporateGifting : Problem
	{
		private class Employee
		{
			public Employee Manager;
			public List<Employee> Subordinates = new List<Employee>();

			public int Gift { get; set; }
			public int Cost { get; set; }

			public void Compute()
			{
				var stack = new Stack<Employee>();
				stack.Push(this);
				while (stack.Count > 0)
				{
					var empties = stack.Peek().Subordinates.Where(e => e.Gift == 0);
					if (empties.Any())
					{
						foreach (var empty in empties)
						{
							stack.Push(empty);
						}
					}
					else
					{
						var subgifts = new HashSet<int>(stack.Peek().Subordinates.Select(s => s.Gift));
						stack.Peek().Gift = Enumerable.Range(1, int.MaxValue).First(i => !subgifts.Contains(i));
						stack.Peek().Cost = stack.Peek().Gift + stack.Peek().Subordinates.Sum(s => s.Cost);
						stack.Pop();
					}
				}
			}
		}

		public override ProblemConfig Config => new ProblemConfig("Corporate Gifting", 2015, 1);

		protected override int LinesPerTestCase { get { return 2; } }

		public override string SolveTestCase(string[] input)
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

			tree[0].Compute();
			return tree[0].Cost.ToString();
		}
	}
}