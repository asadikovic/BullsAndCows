using BaC;
using System;
using System.Diagnostics;

namespace BullsAndCows
{
    static class Program
    {
        private static void Help(string[] args)
        {
            Console.WriteLine("Usage -> BullsAndCows -[help|eval|solve|run|match]");
            Console.WriteLine("Example: BullsAndCows -help");
            Console.WriteLine("Example: BullsAndCows -eval 1234 3154");
            Console.WriteLine("Example: BullsAndCows -solve 1234");
            Console.WriteLine("Example: BullsAndCows -run -random 1000 100");
            Console.WriteLine("Example: BullsAndCows -run -smart 1000 100");
            Console.WriteLine("Example: BullsAndCows -match 1000 100");
        }

        private static void Run(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage -> BullsAndCows -eval -[random|smart] [numGames] [displayStep]");
                return;
            }
            string algo = args[1].ToLowerInvariant();
            int numGamges = args.Length >= 3 ? int.Parse(args[2]) : 10_000;
            int displayStep = args.Length >= 4 ? int.Parse(args[3]) : numGamges / 100;

            IGuesser guesser;
            if (algo == "-random")
                guesser = new RandomGuesser();
            else if (algo == "-smart")
                guesser = new MaxEntropyGuesser();
            else {
                Console.WriteLine("Unknown algorithm");
                return;
            }
            Run(guesser, numGamges, displayStep);
        }
        private static void Run(IGuesser guesser, int numGamges, int displaySteps)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int[] T = new int[10];
            int total = 0;
            for (int i = 1; i <= numGamges; i++)
            {
                var code = Solver.RandomPick(Solver.AllPermitatons);
                int tries = Solver.Solve(code, guesser, false);

                T[tries]++;
                total += tries;

                if (i % displaySteps == 0)
                    Console.WriteLine($"After {i,7} games, average tries: {(float)total / i:F3}");
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            Console.Clear();
            Console.WriteLine($"Algorithm: {guesser}");
            Console.WriteLine($"Total games: {numGamges}");
            Console.WriteLine($"Time elapsed: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");
            Console.WriteLine($"Average tries: {(float)total / numGamges:F3}\n");
            for (int i = 1; i < T.Length; i++)
                if (T[i] > 0)
                    Console.WriteLine($"{T[i],6} solved in {i} " + (i==1 ? "try." : "tries."));
        }

        private static void Eval(string[] args)
        {
            if (args.Length!= 3)
            {
                Console.WriteLine("Usage -> BullsAndCows -eval combo1 combo2");
                return;
            }

            string invalidCodeFormat = "{0} is not valid.";
            var code1 = new Code(args[1]);
            if (!code1.IsValid)
            {
                Console.WriteLine(invalidCodeFormat, code1);
                return;
            }
            var code2 = new Code(args[2]);
            if (!code2.IsValid)
            {
                Console.WriteLine(invalidCodeFormat, code2);
                return;
            }
            var info = code1.CompareWith(code2);
            Console.WriteLine("{0} vs {1} => {2} bulls, {3} cows", args[1], args[2], info.Bulls, info.Cows);
        }

        private static void Solve(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage -> BullsAndCows -solve code");
                return;
            }
            string invalidCodeFormat = "{0} is not valid.";
            var code = new Code(args[1]);
            if (!code.IsValid)
            {
                Console.WriteLine(invalidCodeFormat, code);
                return;
            }
            IGuesser guesser = new RandomGuesser();
            int tries = Solver.Solve(code, guesser, true);
            Console.WriteLine("Solved in {0} tries.", tries);
        }

        private static void Match(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int numGamges = args.Length >= 2 ? int.Parse(args[1]) : 10_000;
            int displayStep = args.Length >= 3 ? int.Parse(args[2]) : numGamges / 100;

            var randomGuesser = new RandomGuesser();
            var smartGuesser = new MaxEntropyGuesser();

            int totalR = 0;
            int totalS = 0;

            int smartWin = 0;
            int randWind = 0;
            int draw = 0;

            for (int i = 1; i <= numGamges; i++)
            {
                var code = Solver.RandomPick(Solver.AllPermitatons);

                int triesRand = Solver.Solve(code, randomGuesser, false);
                int triesSmart = Solver.Solve(code, smartGuesser, false);

                if (triesSmart < triesRand) smartWin++;
                else if (triesRand < triesSmart) randWind++;
                else draw++;

                totalR += triesRand;
                totalS += triesSmart;

                if (i % displayStep == 0)
                {
                    Console.WriteLine($"After {i} games: Smart won {smartWin}, Random won {randWind}, and {draw} draws.");
                }
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            Console.Clear();

            Console.WriteLine("Algorithm: Smart vs Random\n");
            Console.WriteLine($"Total games: {numGamges}");
            Console.WriteLine($"Time elapsed: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}\n");
            Console.WriteLine($"Smart algorithm won {smartWin} games.");
            Console.WriteLine($"Random algorithm won {randWind} games.");
            Console.WriteLine($"There were {draw} draws.\n");
            Console.WriteLine($"Radndom - Average {(float)totalR / numGamges:F3}");
            Console.WriteLine($"Smart - Average {(float)totalS / numGamges:F3}");
        }
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Help(args);
            }
            else
            {
                string commend = args[0].ToLowerInvariant();
                switch (commend)
                {
                    case "-help":
                        Help(args);
                        break;
                    case "-run":
                        Run(args);
                        break;
                    case "-eval":
                        Eval(args);
                        break;
                    case "-solve":
                        Solve(args);
                        break;
                    case "-match":
                        Match(args);
                        break;
                    default:
                        Help(args);
                        break;
                }
             }
        }
    }
}
