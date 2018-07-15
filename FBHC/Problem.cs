using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FBHC
{
    public abstract class Problem
    {
        public abstract ProblemConfig Config { get; }

        public abstract string SolveTestCase(string[] input);

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

		public IEnumerable<string[]> TestCaseInputs(string input)
		{
			string[] lines = SplitLines(input);
			var testCases = int.Parse(lines[0]);
			var lineNumber = 1;
			string[] output = new string[testCases];
			for (int i = 0; i < testCases; i++)
			{
				int testCaseSize = GetLinesInTestCase(lines.Skip(lineNumber));
				string[] testCaseInput = new string[testCaseSize];

				for (int j = 0; j < testCaseInput.Length; j++)
				{
					testCaseInput[j] = lines[lineNumber];
					lineNumber++;
				}

				yield return testCaseInput;
			}
		}

		public string[] SolveInput(string input)
		{
			return TestCaseInputs(input)
				.Select((caseInput, i) => $"Case #{i+1}: {SolveTestCase(caseInput)}")
				.ToArray();
		}
    }
}
