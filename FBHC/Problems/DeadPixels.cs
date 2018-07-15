using System;
using System.Linq;

namespace FBHC.Problems
{
    class DeadPixels:Problem
    {
		public override ProblemConfig Config => new ProblemConfig("DeadPixels", 2013, 1);

		public override string SolveTestCase(string[] input)
        {
            var nums =
                input[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                .Select(t => Convert.ToInt32(t)).ToArray();

            return Solve(nums[0], nums[1], nums[2],
                         nums[3], nums[4], nums[5], nums[6],
                         nums[7], nums[8], nums[9], nums[10]).ToString();
        }

        protected int Solve(int W, int H, int P, int Q, int N, int X, int Y, int a, int b, int c, int d)
        {
            bool[,] p = new bool[W,H];

            p[X, Y] = true;
            for(int i=1;i<N;i++)
            {
                int x = X, y = Y;

                X = (x*a + y*b + 1)%W;
                Y = (x*c + y*d + 1)%H;

                p[X, Y] = true;
            }

            return 0;
        }
    }
}
