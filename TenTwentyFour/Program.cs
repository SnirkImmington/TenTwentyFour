using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TenTwentyFour
{
    class Program
    {
        private static Random rand = new Random();
        private static MarkovNode<string> node(string value)
        {
            return new MarkovNode<string>(value, rand);
        }
        static void Main(string[] args)
        {
            #region Markov Chain

            #region Create Variables
            var hi = node("Hello");
            var howU = node("How are you?");

            var ok = node("Okay");
            var gr8 = node("Great!");

            var sup = node("Anything new?");

            var nutn = node("Nothin'");
            var _code = node("Code!");
            var math = node("Maths :(");

            var sux = node("Sux!");
            var cool = node("Cool!");

            var fun = node("That's fun");
            var bye = node("Bye!");

            var chain = new MarkovChain<string>(hi);

            hi.AddChild(1, hi);
            hi.AddChild(3, howU);

            howU.AddChild(1, howU);
            howU.AddChild(3, gr8);
            howU.AddChild(4, ok);

            ok.AddChild(2, ok);
            ok.AddChild(4, sup);

            gr8.AddChild(1, gr8);
            gr8.AddChild(4, sup);
            gr8.AddChild(2, ok);

            sup.AddChild(4, nutn);
            sup.AddChild(5, _code);
            sup.AddChild(1, math);

            nutn.AddChild(1, sux);
            nutn.AddChild(2, cool);

            _code.AddChild(10, cool);
            _code.AddChild(1, sux);

            math.AddChild(1, cool);
            math.AddChild(10, sux);

            sux.AddChild(10, sup);
            sux.AddChild(10, bye);

            cool.AddChild(5, sup);
            cool.AddChild(5, fun);
            cool.AddChild(1, bye);

            fun.AddChild(10, bye);
            #endregion

            while (true)
            {
                if (chain.current == null) Console.ReadKey();
                var text = chain.current.value;
                Console.WriteLine(text);
                Thread.Sleep(500);
                chain.Next();
            }

            return;

            #endregion

            #region BFI
            Console.WriteLine("Enter BF code, or nothing for \"Hello World\" print code:");
            var code = Console.ReadLine();
            if (code == null || code.Length == 0)
            {
                code = "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++";
                Console.WriteLine("Code = " + code);
            }
            Console.WriteLine("Enter input:");
            var input = Console.ReadLine();
            
            int index = 0;
            var output = BFInterpreter.Output(input, code, null, ref index);
            Console.WriteLine("Output:");
            Console.WriteLine(output);
            Console.ReadLine();
            return;
            #endregion

            #region Weighted RNG
            var rng = new WeightedRNG<string>();
            rng.AddWeight(5, "hey world");
            rng.AddWeight(6, "greetings world");
            rng.AddWeight(10, "hello world");
            rng.AddWeight(1, "yo world");
            int hey = 0, greetings = 0, hello = 0, yo = 0;
            for (int i = 0; i < 70000; i++)
            {
                var rand = rng.GetRandom();
                if (rand.StartsWith("hey")) hey++;
                else if (rand.StartsWith("greetings")) greetings++;
                else if (rand.StartsWith("hello")) hello++;
                else if (rand.StartsWith("yo")) yo++;

                if (i % 7000 == 0)
                    Console.WriteLine("{0}% done...", i / 700);

                //Console.WriteLine(rand);
            }
            Console.WriteLine("Heys (5) | Greet (6) | Hello (10) | Yo's (1)");
            Console.WriteLine("{0}  | {1}  | {2}  | {3}", hey, greetings, hello, yo);
            Console.WriteLine(); float weightsSum = 5 + 6 + 10 + 1;
            Console.WriteLine("{0}  | {1}  | {2}  | {3}", hey / weightsSum, greetings / weightsSum, hello / weightsSum, yo / weightsSum);
            #endregion

            #region Binary Search
            #endregion

            Console.ReadLine();
        }
    }
}
