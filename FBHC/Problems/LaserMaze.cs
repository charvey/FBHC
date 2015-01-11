using System;
using System.Collections.Generic;
using System.Linq;

namespace FBHC.Problems
{
	class LaserMaze : Problem
	{
		protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Laser Maze", 2015, 0); }
        }

		protected override string SolveTestCase(string[] input)
		{
			int[] mn = input[0].Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
			int m = mn[0];
			int n = mn[1];
			int sm = -1, sn = -1, gm = -1, gn = -1;

			char[,] map = new char[m, n];
			bool[,] solid = new bool[m, n];
			bool[,,] open = new bool[m, n, 4];

			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < n; j++)
				{
					map[i, j] = input[1 + i][j];
					solid[i, j] = map[i, j] == '#' || map[i, j] == '<' || map[i, j] == '>' || map[i, j] == 'v' || map[i, j] == '^';
					for (int k = 0; k < 4; k++)
					{
						open[i, j, k] = true;
					}
				}
			}

			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < n; j++)
				{
					switch (map[i, j])
					{
						case '#':
							open[i, j, 0] = false;
							open[i, j, 1] = false;
							open[i, j, 2] = false;
							open[i, j, 3] = false;
							break;
						case '>':
							open[i, j, 0] = false;
							open[i, j, 1] = false;
							open[i, j, 2] = false;
							open[i, j, 3] = false;
							for (int k = j + 1; k < n; k++) { if (solid[i, k]) break; open[i, k, 0] = false; }
							for (int k = i + 1; k < m; k++) { if (solid[k, j]) break; open[k, j, 1] = false; }
							for (int k = j - 1; k >= 0; k--) { if (solid[i, k]) break; open[i, k, 2] = false; }
							for (int k = i - 1; k >= 0; k--) { if (solid[k, j]) break; open[k, j, 3] = false; }
							break;
						case 'v':
							open[i, j, 0] = false;
							open[i, j, 1] = false;
							open[i, j, 2] = false;
							open[i, j, 3] = false;
							for (int k = j + 1; k < n; k++) { if (solid[i, k]) break; open[i, k, 3] = false; }
							for (int k = i + 1; k < m; k++) { if (solid[k, j]) break; open[k, j, 0] = false; }
							for (int k = j - 1; k >= 0; k--) { if (solid[i, k]) break; open[i, k, 1] = false; }
							for (int k = i - 1; k >= 0; k--) { if (solid[k, j]) break; open[k, j, 2] = false; }
							break;
						case '<':
							open[i, j, 0] = false;
							open[i, j, 1] = false;
							open[i, j, 2] = false;
							open[i, j, 3] = false;
							for (int k = j + 1; k < n; k++) { if (solid[i, k]) break; open[i, k, 2] = false; }
							for (int k = i + 1; k < m; k++) { if (solid[k, j]) break; open[k, j, 3] = false; }
							for (int k = j - 1; k >= 0; k--) { if (solid[i, k]) break; open[i, k, 0] = false; }
							for (int k = i - 1; k >= 0; k--) { if (solid[k, j]) break; open[k, j, 1] = false; }
							break;
						case '^':
							open[i, j, 0] = false;
							open[i, j, 1] = false;
							open[i, j, 2] = false;
							open[i, j, 3] = false;
							for (int k = j + 1; k < n; k++) { if (solid[i, k]) break; open[i, k, 1] = false; }
							for (int k = i + 1; k < m; k++) { if (solid[k, j]) break; open[k, j, 2] = false; }
							for (int k = j - 1; k >= 0; k--) { if (solid[i, k]) break; open[i, k, 3] = false; }
							for (int k = i - 1; k >= 0; k--) { if (solid[k, j]) break; open[k, j, 0] = false; }
							break;
						case 'G':
							gm = i;
							gn = j;
							break;
						case 'S':
							sm = i;
							sn = j;
							break;
						case '.':
							break;
					}
				}
			}

			bool[,,] visited = new bool[m, n, 4];
			Queue<Tuple<int, int, int, int>> q = new Queue<Tuple<int, int, int, int>>();
			visited[sm, sn, 0] = true;
			q.Enqueue(new Tuple<int, int, int, int>(sm, sn, 0, 0));
			while (q.Any())
			{
				var x = q.Dequeue();
				if (x.Item1 == gm && x.Item2 == gn) return x.Item4.ToString();
				int t = (x.Item3 + 1) % 4;

				if (x.Item1 + 1 < m && !visited[x.Item1 + 1, x.Item2, t] && open[x.Item1 + 1, x.Item2, t])
				{
					visited[x.Item1 + 1, x.Item2, t] = true;
					q.Enqueue(new Tuple<int, int, int, int>(x.Item1 + 1, x.Item2, t, x.Item4 + 1));
				}
				if (x.Item1 - 1 >= 0 && !visited[x.Item1 - 1, x.Item2, t] && open[x.Item1 - 1, x.Item2, t])
				{
					visited[x.Item1 - 1, x.Item2, t] = true;
					q.Enqueue(new Tuple<int, int, int, int>(x.Item1 - 1, x.Item2, t, x.Item4 + 1));
				}
				if (x.Item2 + 1 < n && !visited[x.Item1, x.Item2 + 1, t] && open[x.Item1, x.Item2 + 1, t])
				{
					visited[x.Item1, x.Item2 + 1, t] = true;
					q.Enqueue(new Tuple<int, int, int, int>(x.Item1, x.Item2 + 1, t, x.Item4 + 1));
				}
				if (x.Item2 - 1 >= 0 && !visited[x.Item1, x.Item2 - 1, t] && open[x.Item1, x.Item2 - 1, t])
				{
					visited[x.Item1, x.Item2 - 1, t] = true;
					q.Enqueue(new Tuple<int, int, int, int>(x.Item1, x.Item2 - 1, t, x.Item4 + 1));
				}
			}
			return "impossible";
		}

		protected override int GetLinesInTestCase(IEnumerable<string> remainingLines)
		{
			return 1 + Convert.ToInt32(remainingLines.First().Split(' ')[0]);
		}
	}
}