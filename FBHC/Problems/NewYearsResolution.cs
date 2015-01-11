using System;
using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
	class NewYearsResolution : Problem
	{
		private struct Macro
		{
			public int P;
			public int C;
			public int F;

			public override bool Equals(object obj)
			{
				var other = (Macro)obj;
				return this.P == other.P && this.C == other.C && this.F == other.F;
			}
		}

		private Macro goal;

		protected override ProblemConfig Config
		{
			get { return new ProblemConfig("New Year's Resolution", 2015, 0); }
		}

		protected override string SolveTestCase(string[] input)
		{
			var goal = getMacros(input.ElementAt(0));

			var foods = input.Skip(2).Select(getMacros).ToArray();

			return canBeMet(goal, foods, new Macro()) ? "yes" : "no";
		}

		private bool canBeMet(Macro goal, Macro[] foods, Macro current, int offset = 0)
		{
			if (offset == foods.Length) return goal.Equals(current);
			var nextFood = foods[offset];
			var newCurrent = new Macro { C = nextFood.C + current.C, F = nextFood.F + current.F, P = nextFood.P + current.P };
			return canBeMet(goal, foods, current, offset + 1) || canBeMet(goal, foods, newCurrent, offset + 1);
		}

		private Macro getMacros(string line)
		{
			int[] macros = line
				.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(m => Convert.ToInt32(m))
				.ToArray();

			return new Macro
			{
				P = macros[0],
				C = macros[1],
				F = macros[2]
			};
		}

		protected override int GetLinesInTestCase(IEnumerable<string> remainingLines)
		{
			return Convert.ToInt32(remainingLines.ElementAt(1)) + 2;
		}
	}
}