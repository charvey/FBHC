using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
	public class FightingTheZombie : Problem
	{
		protected override ProblemConfig Config
		{
			get { return new ProblemConfig("Fighting The Zombie", 2017, 0); }
		}

		protected override int GetLinesInTestCase(IEnumerable<string> remainingLines)
		{
			return 2;
		}

		protected override string SolveTestCase(string[] input)
		{
			var H = int.Parse(input[0].Split(' ')[0]);
			var S = int.Parse(input[0].Split(' ')[1]);

			var spells = input[1].Split(' ').Take(S);

			return spells.Select(s => ChanceOfKilling(s, H)).Max().ToString("F6");
		}

		private struct Spell
		{
			public readonly int X;
			public readonly int Y;
			public readonly int Z;

			public Spell(int x, int y, int z)
			{
				this.X = x;
				this.Y = y;
				this.Z = z;
			}
		}

		private double ChanceOfKilling(string s, int h)
		{
			var spell = Parse(s);

			var offsetH = h - spell.Z;

			if (offsetH < 0)
				return 1;
			else if (offsetH > spell.X * spell.Y)
				return 0;

			var possibilties = GetPossibilities(spell.X, spell.Y);
			var killChance = 0.0;
			for (int i = offsetH; i < possibilties.Length; i++)
				killChance += possibilties[i];
			return killChance;
		}

		private double[] GetPossibilities(int x, int y)
		{
			var possibilities = new double[x * y + 1];
			possibilities[0] = 1;

			for (int _ = 0; _ < x; _++)
			{
				var newPossibilties = new double[x * y + 1];
				for (var i = 0; i < possibilities.Length; i++)
					if (possibilities[i] > 0)
						for (var j = 1; j <= y; j++)
							newPossibilties[i + j] += possibilities[i] / y;
				possibilities = newPossibilties;
			}

			return possibilities;
		}

		private Spell Parse(string s)
		{
			int x, y, z;
			x = int.Parse(s.Split('d')[0]);
			if (s.Contains('-'))
			{
				y = int.Parse(s.Split('d')[1].Split('-')[0]);
				z = -int.Parse(s.Split('d')[1].Split('-')[1]);
			}
			else if (s.Contains('+'))
			{
				y = int.Parse(s.Split('d')[1].Split('+')[0]);
				z = int.Parse(s.Split('d')[1].Split('+')[1]);
			}
			else
			{
				y = int.Parse(s.Split('d')[1]);
				z = 0;
			}
			return new Spell(x, y, z);
		}
	}
}