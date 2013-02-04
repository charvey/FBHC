using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBHC.Problems
{
    class SquishedStatus : Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Squished Status", 2012, 1); }
        }

        protected override int LinesPerCase { get { return 2; } }

        protected override string[] SplitLines(string text)
        {
            return text.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
        }

        protected override string SolveTestCase(string[] input)
        {
            int M = Convert.ToInt32(input[0]);
            string s = input[1];
            ulong[] v = new ulong[s.Length];

            int p;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '0')
                {
                    continue;
                }

                for (int j = 1; j <= 3; j++)
                {
                    if (i + j <= s.Length)
                    {
                        if (int.TryParse(s.Substring(i, j), out p) && p <= M)
                        {
                            v[i] += (i + j < v.Length) ? v[i + j] : 1;
                        }
                    }
                }
                v[i] %= 4207849484;
            }

            return v[0].ToString();
        }
    }
}
