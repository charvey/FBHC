using System;
using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
    public class StudiousStudent : Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Studious Student", 2011, 0); }
        }

        protected override string SolveTestCase(string[] input)
        {
            var tokens = input[0].Split(' ');
            return LeastPermutation(tokens.Skip(1).Take(Convert.ToInt32(tokens[0])).ToArray());
        }

        private string LeastPermutation(string[] p)
        {
            return Permutations(p).Select(c => string.Join(string.Empty, c)).Min();
        }

        private List<T[]> Permutations<T>(T[] collection)
        {
            return Permutations_r(new T[] { }, collection);
        }

        private List<T[]> Permutations_r<T>(T[] seed, T[] collection)
        {
            if (!collection.Any())
                return new List<T[]> { seed };

            var results = new List<T[]>();

            for (int i = 0; i < collection.Length; i++)
            {
                results.AddRange(Permutations_r<T>(seed.Concat(collection.Skip(i).Take(1)).ToArray(), collection.Where((n, s) => s != i).ToArray()));
            }

            return results;
        }
    }
}
