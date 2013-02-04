using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBHC.Problems
{
    class RecoverTheSequence:Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Recover The Sequence", 2012, 1); }
        }

        protected override string SolveTestCase(string[] input)
        {
            throw new NotImplementedException();
        }

        private int checksum(int[] a)
        {
            int result = 1;
            for (int i = 0; i < a.Length; i++)
                result = (31 * result + a[i]) % 1000003;
            return result;
        }
    }
}
