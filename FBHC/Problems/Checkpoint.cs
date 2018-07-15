using System;
using System.Collections.Generic;

namespace FBHC.Problems
{
    class Checkpoint : Problem
    {
		public override ProblemConfig Config => new ProblemConfig("Checkpoint", 2012, 1);

		private static Dictionary<int, int> _min;
        private static Dictionary<int, int> min
        {
            get
            {
                if (_min != null)
                {
                    return _min;
                }

                var newMin = new Dictionary<int, int>();

                newMin[1] = 0;
                for (int a = 2; a <= Math.Sqrt(10000000)*2; a++)
                {
                    newMin[a] = a;
                }

                int[] v = new int[1+(int)Math.Sqrt(10000000)];
                v[0] = 1;
                for (int a = 1; a <= Math.Sqrt(10000000); a++)
                {
                    for (int b = 1; b < a; b++)
                    {
                        v[b] = v[b] + v[b - 1];

                        newMin[a + b] = Math.Min(v[b], newMin[a + b]);
                    }

                    v[a] = 2 * v[a - 1];

                    newMin[a + a] = Math.Min(v[a], newMin[a + a]);
                }

                return _min = newMin;
            }
        }

        public override string SolveTestCase(string[] input)
        {
            int s = Convert.ToInt32(input[0]);

            int minT = int.MaxValue;
            for (int a = 1; a <= Math.Sqrt(s); a++)
            {
                if (s % a != 0)
                {
                    continue;
                }

                int b = s / a;

                minT = Math.Min(minT, min[a] + min[b]);

                Console.Out.WriteLine(s + " " + a + " " + (s / a));
            }

            return minT.ToString();
        }

        private int FindMinT(int a, int b, int min)
        {
            List<int[]> g = new List<int[]> { new [] { 1 } };

            int a_t = 0, b_t = 0;
            for (int t = 1;/*?*/ a_t + b_t < min; t++)
            {
                g.Add(new int[t + 1]);
            }

            return Math.Min(a_t + b_t, min);
        }
    }
}
