using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FBHC
{
	class ProblemCreator
	{
		private string ProjectRoot
		{
			get
			{
				string pwd = Directory.GetCurrentDirectory();
				return Path.Combine(pwd, "../../");
			}
		}

		public void Create(ProblemConfig config)
		{
			createTextFiles(config);
			createCodeFile(config);
		}

		private void createTextFiles(ProblemConfig config)
		{
			var fullDir = Path.Combine(ProjectRoot, config.BaseDirectory);
			if (!Directory.Exists(fullDir))
			{
				Directory.CreateDirectory(fullDir);
			}
			foreach (var path in textFilePaths(config))
			{
				var fullPath = Path.Combine(ProjectRoot, path);
				if (!File.Exists(fullPath))
				{
					File.WriteAllText(fullPath, "");
				}
			}
		}

		private IEnumerable<string> textFilePaths(ProblemConfig config)
		{
			yield return config.SampleInputFileName;
			yield return config.SampleOutputFileName;
			yield return config.InputFileName;
		}

		private void createCodeFile(ProblemConfig config)
		{
			var path = ProjectRoot + "Problems/" + config.FileName + ".cs";
			File.WriteAllText(path, @"
namespace FBHC.Problems
{
	class " + config.FileName + @" : Problem
	{
	}
}");
		}
	}
}
