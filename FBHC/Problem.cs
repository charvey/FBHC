using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FBHC
{
    public abstract class Problem
    {
        protected abstract ProblemConfig Config { get; }
		protected int TestCaseCount { get; set; }

		protected virtual int Setup(string[] lines)
		{
			TestCaseCount = Convert.ToInt32(lines[0]);
			return 1;
		}

        protected abstract string SolveTestCase(string[] input);

        protected virtual int LinesPerTestCase { get { return 1; } }

		protected virtual int GetLinesInTestCase(IEnumerable<string> remainingLines)
		{
			return LinesPerTestCase;
		}

        protected virtual string[] SplitLines(string text)
        {
            return text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        protected string[] Split(string s)
        {
            return s.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void Solve()
        {
            var input = File.ReadAllText(Config.InputFileName);
            var output = SolveInput(input);

            Directory.CreateDirectory("Submissions");
            Directory.CreateDirectory("Submissions/" + Config.FileName);

            File.WriteAllLines("Submissions/" + Config.FileName + "/Output.txt", output);
            File.Copy("Problems/" + Config.FileName + ".cs", "Submissions/" + Config.FileName + "/" + Config.FileName + ".cs", true);
            File.Copy("Problem.cs", "Submissions/" + Config.FileName + "/Problem.cs", true);
            File.Copy("Program.cs", "Submissions/" + Config.FileName + "/Program.cs", true);

            Console.Out.WriteLine(Config.Name + " Solved and Outputted");
        }

        public void Validate()
        {
            var input = File.ReadAllText(Config.SampleInputFileName);
            var testOutput = SolveInput(input);

            var output = File.ReadAllLines(Config.SampleOutputFileName);

            for (int i = 0; i < output.Length; i++)
            {
                if (testOutput.ElementAt(i) != output[i])
                {
                    Console.Out.WriteLine(Config.Name + " Case " + (i + 1) + " is incorrect");
                    Console.Out.WriteLine("Computed:\n\t" + testOutput.ElementAt(i));
                    Console.Out.WriteLine("Correct:\n\t" + output[i]);
                }
                else
                {
                    Console.Out.WriteLine(Config.Name + " Case " + (i + 1) + " is correct");
                }
            }
        }

        private string[] SolveInput(string input)
        {
            string[] lines = SplitLines(input);
			int lineNumber = Setup(lines);
			string[] output = new string[TestCaseCount];
			for (int i = 0; i < TestCaseCount; i++)
            {
				int testCaseSize = GetLinesInTestCase(lines.Skip(lineNumber));
                string[] testCaseInput = new string[testCaseSize];

                for (int j = 0; j < testCaseInput.Length; j++)
                {
                    testCaseInput[j] = lines[lineNumber];
                    lineNumber++;
                }

                string solution = SolveTestCase(testCaseInput);

                output[i] = string.Format("Case #{0}: {1}", i + 1, solution);
            }
            return output;
        }
    }
}
