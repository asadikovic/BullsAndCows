using System;
using System.Collections.Generic;

namespace BaC
{
    public static class Solver
    {
        private static readonly Random rnd = new();
        public static readonly Code[] AllPermitatons = GenerateAllPermutations();

        public static Code[] GenerateAllPermutations()
        {
            var result = new List<Code>();

            for (int i=1; i<10; i++)
            {
                for (int j=1; j<10; j++)
                {
                    if (j == i) continue;

                    for (int k=1; k<10; k++)
                    {
                        if (k == i || k == j) continue;

                        for (int l=1; l<10; l++)
                        {
                            if (l == i || l == j || l == k) continue;

                            result.Add(new Code(i, j, k, l));
                        }
                    }
                }
            }
            return [..result];
        }
        public static Code[] Reduce(Code[] codes, Code code, Info info)
        {
            var result = new List<Code>();

            foreach (var c in codes)
            {
                var i = c.CompareWith(code);
                if (i.Bulls == info.Bulls && i.Cows == info.Cows)
                    result.Add(c);
            }

            return [..result];
        }
        public static Code RandomPick(Code[] codes)
        {
            int n = rnd.Next(codes.Length);

            return codes[n];
        }

        public static int Solve (Code code, IGuesser guesser, bool toDisplay)
        {
            int tries = 0;
            var codes = AllPermitatons.Clone() as Code[];

            var info = new Info { Bulls = 0, Cows = 0 };

            while (info.Bulls != code.Length)
            {
                tries++;
                var guess = guesser.Guess([..codes]);
                info = code.CompareWith(guess);
                codes = Reduce(codes, guess, info);

                if (toDisplay)
                {
                    Console.WriteLine("{0}. Guess: {1} => {2} bulls, {3} cows", tries, guess, info.Bulls, info.Cows);
                }
            }

            return tries;
        }
    }
}
