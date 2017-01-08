using System;
using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
	public class LazyLoading : Problem
	{
		private const double REQUIRED_WEIGHT = 50;

		protected override ProblemConfig Config
		{
			get { return new ProblemConfig("Lazy Loading", 2017, 0); }
		}

		protected override int GetLinesInTestCase(IEnumerable<string> remainingLines)
		{
			return 1 + int.Parse(remainingLines.First());
		}

		protected override string SolveTestCase(string[] input)
		{
			var W = input.Skip(1).Take(int.Parse(input[0])).Select(int.Parse);
			var items = W.OrderByDescending(w => w).ToList();
			int trips = 0;
			while (items.Any())
			{
				var topItem = TakeTop(items);
				var neededStack = Math.Ceiling(REQUIRED_WEIGHT / topItem);
				for (var _ = 2; _ <= neededStack; _++)
					TakeBottom(items);
				if (items.Any() && items.Max() * items.Count < REQUIRED_WEIGHT)
					items.Clear();
				trips++;
			}
			return trips.ToString();
		}

		private static int TakeTop(List<int> items)
		{
			var top = items[0];
			items.RemoveAt(0);
			return top;
		}

		private static int TakeBottom(List<int> items)
		{
			var bottom = items[items.Count - 1];
			items.RemoveAt(items.Count - 1);
			return bottom;
		}
	}
}