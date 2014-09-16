using System;
using System.Collections.Generic;

namespace TenTwentyFour
{
    class WeightedRNG<T> where T : IComparable
    {
        private SortedSet<RNGWeight<T>> weights;
        private int currentMax;
        private Random rand;

        public void AddWeight(int amount, T value)
        {
            weights.Add(new RNGWeight<T>(amount, value));
            currentMax += amount;
        }

        public void AddWeight(RNGWeight<T> weight)
        {
            weights.Add(weight);
            currentMax += weight.Weight;
        }

        public T GetRandom()
        {
            // Default case
            if (currentMax == 0) return default(T);

            var desired = rand.Next(currentMax);
            int current = 0;
            foreach (var weight in weights)
            {
                current += weight.Weight;
                if (current > desired) return weight.Value;
            }
            // Should not be reached.
            throw new IndexOutOfRangeException("Unable to get a value at this range!");
        }

        public WeightedRNG()
        {
            weights = new SortedSet<RNGWeight<T>>();
            rand = new Random(); currentMax = 0;
        }

        public WeightedRNG(Random random)
            : this()
        {
            rand = random;
        }
    }

    class RNGWeight<T> : IComparable where T : IComparable
    {
        public int Weight;
        public T Value;

        public RNGWeight(int weight, T value)
        {
            Weight = weight; Value = value;
        }

        public int CompareTo(object other)
        {
            if (other is RNGWeight<T>)
                return Value.CompareTo(((RNGWeight<T>)other).Value);
            return 0;
        }
    }
}
