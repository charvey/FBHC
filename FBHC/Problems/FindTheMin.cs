using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBHC.Problems
{
    class FindTheMin:Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Find The Min", 2013, 0); }
        }

        protected override int LinesPerTestCase { get { return 2; } }
        
        protected override string SolveTestCase(string[] input)
        {
            if (input[0].Length == 0) return string.Empty;

            var line1 = input[0].Split(' ').Select(s => Convert.ToUInt32(s)).ToArray();
            var line2 = input[1].Split(' ').Select(s => Convert.ToUInt32(s)).ToArray();
            uint n = line1[0], k = line1[1], a = line2[0], b = line2[1], c = line2[2], r = line2[3];

            return _SolveTestCase(n, k, a, b, c, r).ToString();
        }

        private uint _SolveTestCase(uint n, uint k, uint a, uint b, uint c, uint r)
        {
            uint[] m = new uint[k];
            m[0] = a;

            var counts = new Dictionary<uint, int>();
            counts[m[0]] = 1;
            for (uint i = 1; i < k; i++)
            {
                m[i] = (b * m[i - 1] + c) % r;

                if (counts.ContainsKey(m[i % k]))
                {
                    counts[m[i % k]]++;
                }
                else
                {
                    counts[m[i % k]] = 1;
                }
            }

            var prevK = new HashSet<uint>();
            uint min = 0;
            for (uint i = k; i < 2 * k; i++)
            {
                while (counts.ContainsKey(min))
                {
                    min++;
                }

                uint expired = m[i % k];

                m[i % k] = min;

                prevK.Add(m[i % k]);

                if (counts.ContainsKey(m[i % k]))
                {
                    counts[m[i % k]]++;
                }
                else
                {
                    counts[m[i % k]] = 1;
                }

                counts[expired]--;
                if (counts[expired] == 0)
                {
                    counts.Remove(expired);
                }

                min = Math.Min(expired, min + 1);
            }

            //counts.Clear();

            min = k;
            for (uint i = 2*k; i < n; i++)
            {
                while (prevK.Contains(min))
                {
                    min++;
                }

                uint expired = m[i % k];

                m[i % k] = min;

                prevK.Remove(expired);
                prevK.Add(min);

                min = Math.Min(expired, min + 1);
            }

            return m[(n - 1) % k];
        }
    }
}
