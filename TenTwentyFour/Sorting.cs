using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenTwentyFour
{
    static class BubbleSort
    {
        public static void Sort<T>(T[] array) where T : IComparable
        {
            T swap = default(T);

            while (!IsSorted(array))
                for (int i = 0; i < array.Length-1; i++)
                {
                    var compare = array[i].CompareTo(array[i + 1]);
                    if (compare == -1)
                    {
                        swap = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = swap;
                    }
                }
        }

        private static bool IsSorted<T>(T[] array) where T : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
                if (array[i].CompareTo(array[i + 1]) == -1) return false;
            return true;
        }

        private static void WriteArray<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.Write(array[i].ToString() + ' ');
        }
    }
}
