using System;

namespace FBHC.Problems
{
    public class DoubleSquares : Problem
    {
		public override ProblemConfig Config => new ProblemConfig("Double Squares", 2011, 0);

		public override string SolveTestCase(string[] input)
        {
            var tokens = input[0].Split(' ');
            return Squares_TestCase(Convert.ToInt32(tokens[0]));
        }

        private static string Squares_TestCase(int p)
        {
            int c = 0;
            for (int a = 0; ; a++)
            {
                int a2 = a * a, b2 = p - a2;

                if (b2 < a2)
                {
                    break;
                }

                int b = (int)Math.Sqrt(b2);

                if (b * b == b2)
                {
                    c++;
                }
            }
            return c.ToString();
        }
    }
}
