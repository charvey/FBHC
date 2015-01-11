using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBHC.Problems
{
    class SquareDetector:Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Square Detector", 2014, 0); }
        }

        protected override int GetLinesInTestCase(IEnumerable<string> remainingLines)
        {
            return int.Parse(remainingLines.First()) + 1;
        }

        protected override string SolveTestCase(string[] input)
        {
            int N = int.Parse(input[0]);

            bool[][] cells = input.Skip(1).Take(N)
                .Select(l =>
                    l.Select(c => c == '#').ToArray()
                ).ToArray();

            int x0 = 0, y0 = 0, xn = 0, yn = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (cells[i][j])
                    {
                        x0 = i;
                        y0 = j;
                        i = N;
                        j = N;
                    }
                }
            }
            for (int i = N - 1; i >= 0; i--)
            {
                for (int j = N - 1; j >= 0; j--)
                {
                    if (cells[i][j])
                    {
                        xn = i;
                        yn = j;
                        i = 0;
                        j = 0;
                    }
                }
            }
            if (xn - x0 != yn - y0)
            {
                return "NO";
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    bool r;
                    if ((x0 <= i && i <= xn) && (y0 <= j && j <= yn))
                    {
                        r = true;
                    }
                    else
                    {
                        r = false;
                    }
                    if (r != cells[i][j])
                    {
                        return "NO";
                    }
                }
            }

            return "YES";
        }
    }
}
