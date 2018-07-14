using System;
using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
	public class ManicMoving : Problem
	{
		protected override ProblemConfig Config
		{
			get { return new ProblemConfig("Manic Moving", 2017, 1); }
		}

		protected override int GetLinesInTestCase(IEnumerable<string> remainingLines)
		{
			var M = int.Parse(remainingLines.First().Split(' ')[1]);
			var K = int.Parse(remainingLines.First().Split(' ')[2]);
			return 1 + M + K;
		}

		protected override string SolveTestCase(string[] input)
		{
			var N = int.Parse(input[0].Split(' ')[0]);
			var M = int.Parse(input[0].Split(' ')[1]);
			var K = int.Parse(input[0].Split(' ')[2]);
			var A = input.Skip(1).Take(M).Select(l => l.Split(' ')[0]).Select(int.Parse).ToArray();
			var B = input.Skip(1).Take(M).Select(l => l.Split(' ')[1]).Select(int.Parse).ToArray();
			var G = input.Skip(1).Take(M).Select(l => l.Split(' ')[2]).Select(int.Parse).ToArray();
			var S = input.Skip(1).Skip(M).Take(K).Select(l => l.Split(' ')[0]).Select(int.Parse).ToArray();
			var D = input.Skip(1).Skip(M).Take(K).Select(l => l.Split(' ')[1]).Select(int.Parse).ToArray();

			var shortestPaths = ComputeShortestPaths(N, M, A, B, G);
			var cost = LowestCost(shortestPaths, K, S, D);
			return cost.HasValue
				? cost.Value.ToString()
				: (-1).ToString();
		}
		
		private int? LowestCost(int?[,] shortestPaths, int k, int[] s, int[] d, int current = 0, int nextPickup = 0, int nextDropoff = 0)
		{
			if (nextPickup == k && nextDropoff == k) return 0;

			int? pickup = null, dropoff = null;

			var currentLoad = nextPickup - nextDropoff;
			if (nextPickup < k && currentLoad < 2)
			{
				var destination = s[nextPickup] - 1;
				var cost = shortestPaths[current, destination];
				if (cost.HasValue)
				{
					var remaining = LowestCost(shortestPaths, k, s, d, destination, nextPickup + 1, nextDropoff);
					if (remaining.HasValue)
						pickup = cost.Value + remaining.Value;
				}
			}
			if (currentLoad > 0)
			{
				var destination = d[nextDropoff] - 1;
				var cost = shortestPaths[current, destination];
				if (cost.HasValue)
				{
					var remaining = LowestCost(shortestPaths, k, s, d, destination, nextPickup, nextDropoff + 1);
					if (remaining.HasValue)
						dropoff = cost.Value + remaining.Value;
				}
			}

			return MinExisting(pickup, dropoff);
		}

		private int? MinExisting(int? a, int? b)
		{
			if (a.HasValue && b.HasValue) return Math.Min(a.Value, b.Value);
			if (a.HasValue) return a.Value;
			if (b.HasValue) return b.Value;
			return null;
		}

		private int?[,] ComputeShortestPaths(int n, int m, int[] a, int[] b, int[] g)
		{
			var shortestPaths = new int?[n, n];
			for (int i = 0; i < n; i++)
				shortestPaths[i, i] = 0;
			for (int i = 0; i < m; i++)
			{
				var current = shortestPaths[a[i] - 1, b[i] - 1];
				if (!current.HasValue || current.Value > g[i])
				{
					shortestPaths[a[i] - 1, b[i] - 1] = g[i];
					shortestPaths[b[i] - 1, a[i] - 1] = g[i];
				}
			}

			for (var k = 0; k < n; k++)
				for (var i = 0; i < n; i++)
					for (var j = 0; j < n; j++)
					{
						var current = shortestPaths[i, j];
						var bypassA = shortestPaths[i, k];
						var bypassB = shortestPaths[k, j];
						if (bypassA.HasValue && bypassB.HasValue && (current.HasValue ? current.Value : int.MaxValue) > bypassA.Value + bypassB.Value)
							shortestPaths[i, j] = bypassA.Value + bypassB.Value;
					}

			return shortestPaths;
		}
	}
}
