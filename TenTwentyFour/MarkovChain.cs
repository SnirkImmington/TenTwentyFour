using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenTwentyFour
{
    class MarkovChain<T> where T : IComparable
    {
        MarkovNode<T> head;

        public MarkovNode<T> current;

        public MarkovChain(MarkovNode<T> head)
        {
            this.head = head; current = head;
        }

        public MarkovNode<T> Next()
        {
            if (current == null) return null;
            current = current.Next();
            return current;
        }
    }
    class MarkovNode<T> : IComparable where T : IComparable
    {
        public T value;
        private WeightedRNG<MarkovNode<T>> children;

        public int CompareTo(object other)
        {
            if (other is MarkovNode<T>)
                return value.CompareTo(((MarkovNode<T>)other).value);
            return 0;
        }

        public void AddChild(int amount, MarkovNode<T> child)
        {
            children.AddWeight(amount, child);
        }

        public MarkovNode<T> Next()
        {
            return children.GetRandom();
        }

        public MarkovNode(T value, Random rand)
        {
            this.value = value;
            children = new WeightedRNG<MarkovNode<T>>(rand);
        }

        public MarkovNode(T value, WeightedRNG<MarkovNode<T>> childs)
        {
            this.value = value; children = childs;
        }
    }
}
