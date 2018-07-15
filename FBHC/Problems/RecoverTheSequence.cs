using System;

namespace FBHC.Problems
{
    class RecoverTheSequence:Problem
    {
		public override ProblemConfig Config => new ProblemConfig("Recover The Sequence", 2012, 1);

		public override string SolveTestCase(string[] input)
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
