using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FBHC.Problems
{
    class CardGame:Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("CardGame", 2013, 1); }
        }

        protected override int LinesPerTestCase
        {
            get { return 2; }
        }

        protected override string SolveTestCase(string[] input)
        {
            int n = Convert.ToInt32(input[0].Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries)[0]);
            uint k = Convert.ToUInt32(input[0].Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries)[1]);

            ulong[] a = input[1].Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries)
                .Take(n).Select(t => Convert.ToUInt64(t)).ToArray();

            return Solve(k, a).ToString(CultureInfo.InvariantCulture);
        }

        protected ulong Solve(uint k, ulong[] a)
        {
            const ulong M = 1000000007;
            a = a.OrderByDescending(i => i).ToArray();

            ulong s = 0;
            var y = new List<ulong>();
            for (int i = 0; i <= a.Length - k; i++)
            {
                var x = nCk((ulong)(a.Length - i) - 1, k - 1);

                if(x.Item2!=1)
                {
                    y.Add(x.Item2);
                }

                s += a[i]*x.Item1;


                for (int j = 0; j < y.Count;j++)
                {
                    if(s%y[j]==0)
                    {
                        s /= y[j];
                        y.RemoveAt(j);
                        j--;
                    }
                }

                    s %= M;
            }
            Console.Out.WriteLine(y.Count);
            return s;
        }

        private Tuple<ulong,ulong> nCk(ulong n, ulong k)
        {

            ulong r = 1;
            ulong r2 = 1;
            for(ulong i=Math.Max(n-k,k)+1;i<=n;i++)
            {
                r *= i;
            }

            for (ulong i = Math.Min(n - k, k); i >= 1; i--)
            {
                if (r % i == 0)
                    r /= i;
                else
                    r2 *= i;
            }
            return new Tuple<ulong, ulong>(r, r2);
        }
    }
}
