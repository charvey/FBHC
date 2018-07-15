using System;
using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
    class Security:Problem
    {
		public override ProblemConfig Config => new ProblemConfig("Security", 2013, 1);

		protected override int LinesPerTestCase
        {
            get { return 3; }
        }

        public override string SolveTestCase(string[] input)
        {
            int m = Convert.ToInt32(input[0]);
            string k1 = input[1];
            string k2 = input[2];
            int l = k2.Length/m;

            string min = null;

            string[] sections = new string[m];

            for (int i = 0; i < m; i++)
            {
                sections[i] = k2.Substring(i*l, l);
            }

            sections = sections.OrderBy(s => s).ToArray();

            min = LeastPermutation(sections, k1, l);

            return min ?? "IMPOSSIBLE";
        }

        private string LeastPermutation(string[] p, string k1, int l)
        {
            string min = null;
            foreach (var perm in Permutations(p, k1, l))
            {
                if (min == null || string.Compare(perm, min) < 0)
                {
                    min = perm;
                }
            }

            return min;
        }

        private List<string> Permutations(string[] collection, string k1, int l)
        {
            return Permutations_r(string.Empty, collection, k1, l);
        }

        private List<string> Permutations_r(string seed, string[] collection, string k1, int l)
        {
            if (!collection.Any())
                return new List<string> {seed};

            var results = new List<string>();

            for (int i = 0; i < collection.Length; i++)
            {
                var next = collection[i].ToCharArray();

                bool poss = true;
                for (int j = 0; j < l; j++)
                {
                    char c = k1[seed.Length + j];
                    if (c == '?' && next[j] == '?')
                    {

                    }
                    else if(c==next[j])
                    {
                        
                    }
                    else if (next[j] == '?')
                    {
                        next[j] = c;
                    }
                    else
                    {
                        poss = false;
                        break;
                    }
                }
                if(!poss)
                {
                    continue;
                }

                var p = Permutations_r(string.Empty, collection.Where((n, s) => s != i).ToArray(), k1.Substring(l), l);

                foreach(var e in p)
                {
                    if (e != null)
                    {
                        results.Add(seed + new string(next) + e);
                    }
                }
            }

            return results;
        }
    }
}
