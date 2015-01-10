﻿using System;
using System.Linq;

namespace FBHC.Problems
{
    public class Labelmaker : Problem
    {
        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Labelmaker", 2014, 1); }
        }

        protected override string SolveTestCase(string[] input)
        {
            input=Split(input[0]);
            string L = input[0];
            ulong l=(uint)L.Length;
            ulong N = ulong.Parse(input[1]);

            ulong i=l;
            while(i<=N){
                i*=l;
            }

			throw new NotImplementedException(); 
        }
    }
}
