using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FBHC.Problems
{
    class DeadPixels:Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("DeadPixels", 2013, 1); }
        }

        protected override string SolveTestCase(string[] input)
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
