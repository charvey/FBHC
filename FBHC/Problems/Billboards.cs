using System;
using System.Linq;

namespace FBHC.Problems
{
    public class Billboards:Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Billboards", 2012, 0); }
        }

        protected override string SolveTestCase(string[] input)
        {
            var tokens = input[0].Split(' ');
            return FitToBillboard(Convert.ToInt32(tokens[0]), Convert.ToInt32(tokens[1]), tokens.Skip(2).ToArray()).ToString();
        }

        private int FitToBillboard(int w, int h, string[] words)
        {
            for (int f = Math.Min(w, h); f > 0; f--)
            {
                if (FitsBillboard(f, w, h, words))
                {
                    return f;
                }
            }

            return 0;
        }

        private bool FitsBillboard(int f, int w, int h, string[] words)
        {
            int _w, _h, i;

            _w = _h = i = 0;

            while (i < words.Length)
            {
                if (words[i].Length * f <= w - _w)
                {
                    _w += words[i].Length * f;
                    i++;
                    _w += f;
                }
                else
                {
                    _w = 0;
                    _h += f;
                }

                if (_h + f > h)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
