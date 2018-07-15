using System;
using System.Linq;

namespace FBHC.Problems
{
    public class PegGame : Problem
    {
		public override ProblemConfig Config => new ProblemConfig("Peg Game", 2011, 0);

		public override string SolveTestCase(string[] input)
        {
            var tokens = input[0].Split(' ');
            var pi = tokens.Select(x => Convert.ToInt32(x)).ToArray();
            return Pegs_TestCase(pi[0], pi[1], pi[2], pi.Skip(4).Take(2 * pi[3]).ToArray());
        }

        private static string Pegs_TestCase(int r, int c, int k, int[] m)
        {
            var board = Pegs_BuildBoard(r, c, m);

            for (int i = 0; i < board.GetLength(0); i++)
            {
                //Console.WriteLine("|" + new string(Enumerable.Range(0, board.GetLength(1)).Select(j => board[i, j] ? 'X' : ' ').ToArray()) + "|");
            }

            var x = Pegs_Best(k, board);

			return $"{x.Item1} {x.Item2:F6}";
        }

        private static bool[,] Pegs_BuildBoard(int r, int c, int[] m)
        {
            bool[,] pegs = new bool[r, c * 2 - 1];

            for (int i = 0; i < r; i++)
            {
                pegs[i, 0] = true;
                for (int j = 1; j < c * 2 - 2; j++)
                {
                    pegs[i, j] = i % 2 == j % 2;
                }
                pegs[i, c * 2 - 2] = true;
            }

            for (int i = 0; i < m.Length / 2; i++)
            {
                int ri = m[i * 2 + 0], ci = m[i * 2 + 1];

                pegs[ri, ci * 2 + ri % 2] = false;
            }

            return pegs;
        }

        private static Tuple<int, double> Pegs_Best(int k, bool[,] board)
        {
            double[] chances = new double[board.GetLength(1)];
            chances[k * 2 + 1] = 1;

            //Console.Out.WriteLine(string.Join("|", chances.Select(c => c.ToString("0.000"))) + "\t|" + string.Join("|", Enumerable.Range(0, board.GetLength(1)).Select(j => board[board.GetLength(0) - 1, j] ? "X" : " ")) + "|");

            for (int i = board.GetLength(0) - 2; i >= 0; i--)
            {
                double[] newChances = new double[board.GetLength(1)];

                for (int j = 0; j < newChances.Length; j++)
                {
                    if (board[i, j])
                    {
                        newChances[j] = 0;
                        continue;
                    }

                    if (board[i + 1, j])
                    {
                        int f = 0;
                        if (!board[i + 1, j - 1])
                        {
                            newChances[j] += chances[j - 1];
                            f++;
                        }
                        if (!board[i + 1, j + 1])
                        {
                            newChances[j] += chances[j + 1];
                            f++;
                        }
                        newChances[j] /= f;
                    }
                    else
                    {
                        newChances[j] = chances[j];
                    }
                }

                chances = newChances;

                //Console.Out.WriteLine(string.Join("|", chances.Select(c => c.ToString("0.000"))) + "\t|" + string.Join("|", Enumerable.Range(0, board.GetLength(1)).Select(j => board[i, j] ? "X" : " ")) + "|");
            }

            int m = 0;
            for (int i = 1; i < (board.GetLength(1) - 1) / 2; i++)
            {
                if (chances[i * 2 + 1] > chances[m * 2 + 1])
                {
                    m = i;
                }
            }

            return new Tuple<int, double>(m, chances[m * 2 + 1]);
        }
    }
}
