using System.Collections.Generic;
using System.Text;

namespace TenTwentyFour
{
    static class BFInterpreter
    {
        public static string Output(string input, string code, List<char> chars, ref int index)
        {
            if (chars == null)
            {
                chars = new List<char>(50);
                for (int i = 0; i < 50; i++)
                    chars.Add((char)0);
            }
            var outBuilder = new StringBuilder();

            for (int i = 0; i < code.Length; i++)
            {
                switch (code[i])
                {
                    case '+':
                        chars[index] = (char)(chars[index] + 1);
                        continue;
                    case '-':
                        chars[index] = (char)(chars[index] - 1);
                        continue;

                    case '>':
                        index++; continue;
                    case '<':
                        index--; continue;

                    case '.': outBuilder.Append(chars[index]); continue;

                    case ',':
                        char fromIn; // Length checks here
                        if (index > input.Length) fromIn = (char)0;
                        else fromIn = input[index];
                        chars[index] = fromIn;
                        continue;

                    case '[':
                        var loopText = IndexTillChar(code, i);
                        while (chars[index] != (char)0)
                            outBuilder.Append(Output(input, loopText, chars, ref index));
                        i += loopText.Length;
                        continue;
                }
            }
            return outBuilder.ToString();
        }
        
        private static string IndexTillChar(string input, int index)
        {
            int i = index+1; int current = 1;
            for (; i < input.Length; i++)
            {
                if (input[i] == '[') current++;
                else if (input[i] == ']')
                {
                    current--;
                    if (current == 0) break;
                }
            }
            return input.Substring(index+1, i - index);
        }

        private static char Get(int index, List<char> values)
        {
            if (index > values.Count)
            {
                values.Add((char)0);
                return (char)0;
            }
            return values[index];
        }
    }
}
