using System;

namespace BaC
{
    public class MaxEntropyGuesser: IGuesser
    {
        public override string ToString()
        {
            return "Maximum Entropy Selection";
        }
        public Code Guess(Code[] codes) 
        {
            Code pick = Solver.RandomPick(codes);

            if (codes.Length==3024) return pick;
            
            double maxH = 0.0;
            for (int i = 0; i < codes.Length; i++)
            {
                var code = codes[i];
                double H = Entropy(codes, code, i);
                if (H > maxH)
                {
                    maxH = H;
                    pick = code;
                }
            }

            return pick;
        }

        public static double Entropy(Code[] codes, Code code, int n)
        {
            int length = code.Length;
            var T = new int [length, length+1];

            for (int i = 0; i < codes.Length; i++)
            {
                if (i == n) continue; // skip the code itself

                var info = code.CompareWith(codes[i]);
                T[info.Bulls, info.Cows]++;
            }

            double H = 0;

            for (int i=0; i<length; i++)
            {
                for (int j=0; j<length+1; j++)
                {
                    int count = T[i, j];
                    if (count == 0) continue;
                    double p = (double)count / codes.Length;
                    H += -p * Math.Log2(p);
                }
            }

            return H;
        }
    }
}
