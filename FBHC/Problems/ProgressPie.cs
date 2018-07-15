using System;

namespace FBHC.Problems
{
	class ProgressPie : Problem
	{
		private const int cX = 50;
		private const int WIDTH = 100;
		private const int cY = 50;
		private const int HEIGHT = 100;

		public override ProblemConfig Config => new ProblemConfig("Progress Pie", 2017, 0);

		public override string SolveTestCase(string[] input)
		{
			var P = int.Parse(input[0].Split(' ')[0]);
			var X = int.Parse(input[0].Split(' ')[1]);
			var Y = int.Parse(input[0].Split(' ')[2]);

			return PixelOn(X, Y, P) ? "black" : "white";
		}

		private bool PixelOn(int x, int y, int p)
		{
			return InsideCircle(x, y) && InsideArc(p, x, y);
		}

		private bool InsideCircle(int x, int y)
		{
			return Math.Sqrt(Math.Pow(Math.Abs(x - cX), 2) + Math.Pow(Math.Abs(y - cY), 2)) <= (HEIGHT - cY);
		}

		private bool InsideArc(int p, int x, int y)
		{
			var angle = (Math.Atan2(y - cY, x - cX) + 1.5 * Math.PI) % (2 * Math.PI);
			var percent = 100 * (1 - (angle / (2 * Math.PI)));
			return p >= percent;
		}
	}
}