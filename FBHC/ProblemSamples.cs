using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FBHC
{
	[TestFixture]
	[Parallelizable(ParallelScope.Children)]
	class ProblemSamples
	{
		static ProblemSamples()
		{
			var dir = Path.GetDirectoryName(typeof(Problem).Assembly.Location);
			Directory.SetCurrentDirectory(dir);
		}

		private static IEnumerable<Problem> AllProblems
		{
			get
			{
				return typeof(Problem).Assembly.GetTypes()
					.Where(t => typeof(Problem).IsAssignableFrom(t))
					.Where(t => !t.IsAbstract)
					.Select(t => Activator.CreateInstance(t) as Problem);
			}
		}

		public static IEnumerable<TestCaseData> TestCases
		{
			get
			{
				foreach (var problem in AllProblems)
				{
					var sampleInput = File.ReadAllText(problem.Config.SampleInputFileName);
					var testCaseInputs = problem.TestCaseInputs(sampleInput).ToList();
					var outputs = File.ReadAllText(problem.Config.SampleOutputFileName)
						.Split(new[] { "Case #" }, StringSplitOptions.RemoveEmptyEntries)
						.Select(o => o.Split(new[] { ':' }, 2)[1].Trim())
						.ToArray();

					Assert.That(testCaseInputs, Has.Count.EqualTo(outputs.Length));

					for (var i = 0; i < testCaseInputs.Count; i++)
						yield return new TestCaseData(problem, testCaseInputs[i])
							.Returns(outputs[i])
							.SetName($"{problem.Config.Name} {i + 1}/{testCaseInputs.Count}");
				}
			}
		}

		[TestCaseSource(nameof(TestCases))]
		[Timeout(5 * 1000)]
		public string SampleCases(Problem problem, string[] input) => problem.SolveTestCase(input);

		[TestCaseSource(nameof(AllProblems))]
		[Timeout(10 * 1000)]
		public void SampleInput(Problem problem)
		{
			var sampleInput = File.ReadAllText(problem.Config.SampleInputFileName);

			var actualOutput = problem.SolveInput(sampleInput);

			var expectedOutput = File.ReadAllLines(problem.Config.SampleOutputFileName);
			CollectionAssert.AreEqual(expectedOutput, actualOutput);
		}
	}
}
