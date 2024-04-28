namespace rac.so.bytehaven
{
    public class BinaryHeap<T>
    {
        /* An implementation of the Heap data structure. MIT License.
        Made with ♠ by Racso. https://rac.so | https://github.com/Racso */

        private readonly int[] priorities;
        private readonly T[] data;
        private readonly Func<int, int, int> priorityComparer;
        private readonly Func<T, T, bool> dataEqualityComparer;

        public int Count { get; private set; }

        public BinaryHeap(int capacity, IComparer<int> comparer, IEqualityComparer<T> equalityComparer)
            : this(capacity, comparer.Compare, equalityComparer.Equals)
        {
        }

        public BinaryHeap(int capacity, Func<int, int, int> priorityComparer, Func<T, T, bool> dataEqualityComparer)
        {
            data = new T[capacity];
            priorities = new int[capacity];
            this.priorityComparer = priorityComparer;
            this.dataEqualityComparer = dataEqualityComparer;
        }

        public (T Value, int Priority) Peek()
            => (data[0], priorities[0]);

        public void Push(T item, int priority)
        {
            data[Count] = item;
            priorities[Count] = priority;
            SiftUp(Count);
            Count++;
        }

        public (T Value, int Priority) Pop()
        {
            T result = data[0];
            int priority = priorities[0];
            data[0] = data[Count - 1];
            priorities[0] = priorities[Count - 1];
            Count--;
            SiftDown(0);
            return (result, priority);
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (dataEqualityComparer(data[i], item))
                {
                    data[i] = data[Count - 1];
                    priorities[i] = priorities[Count - 1];
                    Count--;

                    if (i == 0 || priorityComparer(priorities[i], priorities[(i - 1) / 2]) >= 0)
                        SiftDown(i);
                    else
                        SiftUp(i);

                    return true;
                }
            }

            return false;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (dataEqualityComparer(data[i], item))
                    return true;
            }

            return false;
        }

        private void SiftUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (priorityComparer(priorities[index], priorities[parent]) >= 0)
                    break;

                Swap(index, parent);
                index = parent;
            }
        }

        private void SiftDown(int index)
        {
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;

                int smallest = index;

                if (left < Count && priorityComparer(priorities[left], priorities[smallest]) < 0)
                    smallest = left;
                if (right < Count && priorityComparer(priorities[right], priorities[smallest]) < 0)
                    smallest = right;

                if (smallest == index)
                    break;

                Swap(index, smallest);
                index = smallest;
            }
        }

        private void Swap(int a, int b)
        {
            (data[a], data[b]) = (data[b], data[a]);
            (priorities[a], priorities[b]) = (priorities[b], priorities[a]);
        }
    }
}