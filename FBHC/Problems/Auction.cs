using System;
using System.Linq;

namespace FBHC.Problems
{
    public class Auction : Problem
    {
        //http://pastebin.com/raw.php?i=X2ZB2dY3

        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Auction", 2012, 0); }
        }

        protected override string SolveTestCase(string[] input)
        {
            var tokens = input[0].Split(' ');
            var p = tokens.Skip(1).Select(t => Convert.ToInt32(t)).ToArray();
            var x = TerribleDealsBargains(Convert.ToInt64(tokens[0]), p[0], p[1], p[2], p[3], p[4], p[5], p[6], p[7]);
            return string.Format("{0} {1}", x.Item1, x.Item2);
        }

        protected Tuple<int, int> TerribleDealsBargains(long N, int p1, int w1, int M, int K, int A, int B, int C, int D)
        {
            var g = BuildGrid(N, p1, w1, M, K, A, B, C, D);

            int t = TerribleDeals(g);
            int b = Bargains(g);

            return new Tuple<int, int>(t, b);
        }

        private int[,] BuildGrid(long N, int p1, int w1, int M, int K, int A, int B, int C, int D)
        {
            int[,] grid = new int[M + 1, K + 1];

            int pi = p1;
            int wi = w1;

            for (int i = 1; i <= N; i++)
            {
                grid[pi, wi]++;

                pi = (A * pi + B) % M + 1;
                wi = (C * wi + D) % K + 1;
            }

            for (int i = grid.GetLength(0) - 1; i >= 1; i--)
            {
                for (int j = 1; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }

            return grid;
        }

        private int TerribleDeals(int[,] grid)
        {
            int wc = 0,total=0;

            for (int w = grid.GetLength(1) - 1; w > 0; w--)
            {
                for (int c = grid.GetLength(0) - 1; c > wc; c--)
                {
                    if (grid[c, w] != 0)
                    {
                        total += grid[c, w];
                        wc = c;
                    }
                }
            }

            return total;
        }

        private int Bargains(int[,] grid)
        {
            int wc = grid.GetLength(0), total = 0;

            for (int w = 1; w < grid.GetLength(1); w++)
            {
                for (int c = 1; c < wc; c++)
                {
                    if (grid[c, w] != 0)
                    {
                        total += grid[c, w];
                        wc = c;
                    }
                }
            }

            return total;
        }
    }
}
