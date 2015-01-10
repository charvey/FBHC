using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBHC.Problems
{
    class BasketballGame : Problem
    {
        class Player
        {
            public string Name;
            public int ShotPercentage;
            public int Height;
            public int GameTime = 0;
            public int DraftOrder;
        }

        protected override ProblemConfig Config
        {
            get { return new ProblemConfig("Basketball Game", 2014, 0); }
        }

        protected override int GetLinesInTestCase(string nextLine)
        {
            return int.Parse(Split(nextLine)[0]) + 1;
        }

        protected override string SolveTestCase(string[] input)
        {
            string[] nmp = Split(input[0]);
            int N = int.Parse(nmp[0]),
                M = int.Parse(nmp[1]),
                P = int.Parse(nmp[2]);

            Player[] players = input.Skip(1)
                .Select(Split).Select(l => new Player
                {
                    Name = l[0],
                    ShotPercentage = int.Parse(l[1]),
                    Height = int.Parse(l[2])
                }).ToArray();

            players = players.OrderByDescending(p => p.ShotPercentage * 1000 + p.Height).ToArray();

            for (int i = 0; i < players.Length; i++)
            {
                players[i].DraftOrder = i + 1;
            }

            Player[] t1 = players.Where(p => p.DraftOrder % 2 == 1).ToArray(),
                t2 = players.Where(p => p.DraftOrder % 2 == 0).ToArray();

            for (int i = 1; i <= M; i++)
            {
                t1 = update(t1, P);
                t2 = update(t2, P);
            }

            return string.Join(" ", t1.Take(P).Concat(t2.Take(P))
                .Select(p => p.Name).OrderBy(s => s));
        }

        private Player[] update(Player[] t, int P)
        {
            for (int i = 0; i < P; i++)
            {
                t[i].GameTime++;
            }

            var inGame = t.Take(P).ToList();
            var outGame = t.Skip(P).ToList();

            int mostTime = inGame.Max(p => p.GameTime);
            Player nextOut = inGame.Where(p => p.GameTime == mostTime).OrderByDescending(p => p.DraftOrder).First();

            inGame.Remove(nextOut);
            outGame.Add(nextOut);

            int leastTime = outGame.Min(p => p.GameTime);
            Player nextIn = outGame.Where(p => p.GameTime == leastTime).OrderBy(p => p.DraftOrder).First();

            outGame.Remove(nextIn);
            inGame.Add(nextIn);

            return inGame.Concat(outGame).ToArray();
        }
    }
}
