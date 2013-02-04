using System;
using FBHC.Problems;

namespace FBHC
{
    class Program
    {
        static void Main(string[] args)
        {
            var problems = new Problem[] {
                new Checkpoint()
            };

            foreach (var problem in problems)
            {
                problem.Validate();
            }

            Console.In.Read();
        }
    }
}
