using System.Linq;

namespace FBHC.Problems
{
	class CookingTheBooks : Problem
	{
		public override ProblemConfig Config => new ProblemConfig("Cooking The Books", 2015, 0);

		public override string SolveTestCase(string[] input)
		{
			char[] digits = input[0].ToArray();

			var min = digits.ToArray();
			var max = digits.ToArray();

			for (int i = 0; i < digits.Length; i++)
			{
				for (int j = i + 1; j < digits.Length; j++)
				{
					if (i == 0 && digits[j] == '0')
					{
						continue;
					}

					char[] t = digits.ToArray();
					t[i] = digits[j];
					t[j] = digits[i];

					if (lt(t, min))
					{
						min = t;
					}
					if (gt(t, max))
					{
						max = t;
					}
				}
			}

			return string.Join("", min) + " " + string.Join("", max);
		}

		private bool lt(char[] t, char[] v)
		{
			for (int i = 0; i < t.Length; i++)
			{
				if (t[i] < v[i])
				{
					return true;
				}
				else if (t[i] > v[i])
				{
					return false;
				}
			}
			return false;
		}

		private bool gt(char[] t, char[] v)
		{
			for (int i = 0; i < t.Length; i++)
			{
				if (t[i] > v[i])
				{
					return true;
				}
				else if (t[i] < v[i])
				{
					return false;
				}
			}
			return false;
		}
	}
}
