using System;
using System.Globalization;

namespace BaC
{
    public class Code
    {
        public int Length { get; }
        private readonly int[] code;

        public override string ToString()
        {
            return string.Join("", code);
        }

        public int this [int index]
        {
            get
            {                
                return code[index];
            }
        }

        public Code(params int[] code)
        {
            this.code = code;
            this.Length = code.Length;
        }

        public Code(string code)
        {
            this.code = new int[code.Length];
            for (int i = 0; i < code.Length; i++)
            {
                if (!int.TryParse(code[i].ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out this.code[i]) || this.code[i] < 1 || this.code[i] > 9)
                    throw new ArgumentException("Code must consist of digits from 1 to 9.");
            }
            this.Length = code.Length;
        }
        public bool IsValid
        {
            get
            {
                if (code.Length != 4) return false; // Must be exactly 4 digits

                for (int i = 0; i < code.Length; i++)
                {
                    if (code[i] < 1 || code[i] > 9) return false;
                    for (int j = i + 1; j < code.Length; j++)
                        if (code[i] == code[j]) return false;
                }
                return true;
            }
        }

        public Info CompareWith(Code that)
        {
            var result = new Info { Bulls = 0, Cows = 0 };

            for (int i = 0; i < code.Length; i++)
                for (int j = 0; j < code.Length; j++)
                    if (this[i] == that[j])
                        if (i == j)
                            result.Bulls++;
                        else
                            result.Cows++;

            return result;
        }
    }
}
