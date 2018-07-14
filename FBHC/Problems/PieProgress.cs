using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
	public class PieProgress : Problem
	{
		protected override ProblemConfig Config
		{
			get { return new ProblemConfig("Pie Progress", 2017, 1); }
		}

		protected override int GetLinesInTestCase(IEnumerable<string> remainingLines)
		{
			return 1 + int.Parse(remainingLines.First().Split(' ')[0]);
		}

		private class ShopDay
		{
			public Queue<int> Remaining { get; set; }
			public List<int> Bought { get; set; }
		}

		protected override string SolveTestCase(string[] input)
		{
			var N = int.Parse(input[0].Split(' ')[0]);
			var M = int.Parse(input[0].Split(' ')[1]);
			var C = input.Skip(1).Take(N).Select(l => l.Split(' ').Take(M).Select(int.Parse).ToArray()).ToArray();

			var shopDays = C.Select(d => new ShopDay { Remaining = new Queue<int>(d.OrderBy(c => c)), Bought = new List<int>() }).ToList();
			for(int day = 1; day <= N; day++)
			{
				BuyCheapestPie(shopDays.Take(day).ToList());
			}

			return shopDays.Sum(d => d.Bought.Sum() + d.Bought.Count * d.Bought.Count).ToString();
		}

		private void BuyCheapestPie(List<ShopDay> days)
		{
			ShopDay dayToBuyFrom = null;
			var cheapest = int.MaxValue;
			foreach(var day in days)
			{
				if (!day.Remaining.Any()) continue;
				var addedPrice = day.Remaining.Peek() + 2 * day.Bought.Count + 1;
				if (addedPrice < cheapest)
				{
					dayToBuyFrom = day;
					cheapest = addedPrice;
				}
			}
			dayToBuyFrom.Bought.Add(dayToBuyFrom.Remaining.Dequeue());
		}
	}
}
