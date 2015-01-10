using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBHC.Problems
{
    class Tennison : Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Tennison", 2014, 0); }
        }

        protected override string SolveTestCase(string[] input)
        {
            Console.WriteLine("New Case");
            input = input[0].Split(new char[]{}, StringSplitOptions.RemoveEmptyEntries);

            int k = int.Parse(input[0]);
            double ps = double.Parse(input[1]);
            double pr = double.Parse(input[2]);
            double pi = double.Parse(input[3]);
            double pu = double.Parse(input[4]);
            double pw = double.Parse(input[5]);
            double pd = double.Parse(input[6]);
            double pl = double.Parse(input[7]);

            double e = simulate(k, 0, 0, ps, pr, pi, pu, pw, pd, pl);
            return Math.Round(e, 6).ToString("0.000000");
        }

        protected double simulate(int k, int w, int l, double ps, double pr, double pi, double pu, double pw, double pd, double pl)
        {
            if (w >= k) return 1;
            if (l >= k) return 0;

            double win=pi*ps+(1-pi)*pr;

            return win * simulate(k, w + 1, l, ps, pr, Math.Min(0, pi + pw * pu), pu, pw, pd, pl) +
                (1 - win) * simulate(k, w, l + 1, ps, pr, Math.Max(1, pi - pd * pl), pu, pw, pd, pl);
        }
    }
}
