namespace rac.so.bytehaven
{
    public class BinaryHeapWithMap<T>
    {
        /* An implementation of the Heap+Map data structure. MIT License.
        Made with ♠ by Racso. https://rac.so | https://github.com/Racso */

        private readonly int[] priorities;
        private readonly T[] data;
        private int count;
        private readonly Dictionary<T, int> map;
        private readonly Func<int, int, int> priorityComparer;

        public BinaryHeapWithMap(int capacity, IComparer<int> comparer)
            : this(capacity, comparer.Compare)
        {
        }

        public BinaryHeapWithMap(int capacity, Func<int, int, int> priorityComparer)
        {
            data = new T[capacity];
            priorities = new int[capacity];
            map = new Dictionary<T, int>(capacity);
            this.priorityComparer = priorityComparer;
        }

        public int Count => count;

        public (T Value, int Priority) Peek()
            => (data[0], priorities[0]);

        public void Push(T item, int priority)
        {
            data[count] = item;
            priorities[count] = priority;
            map[item] = count;
            SiftUp(count);
            count++;
        }

        public (T Value, int Priority) Pop()
        {
            T result = data[0];
            int priority = priorities[0];
            data[0] = data[count - 1];
            priorities[0] = priorities[count - 1];
            map[data[0]] = 0;
            count--;
            map.Remove(result);
            SiftDown(0);
            return (result, priority);
        }

        public bool Remove(T item)
        {
            if (!map.TryGetValue(item, out int index))
                return false;

            data[index] = data[count - 1];
            priorities[index] = priorities[count - 1];
            map[data[index]] = index;
            count--;
            map.Remove(item);

            if (index == 0 || priorityComparer(priorities[index], priorities[(index - 1) / 2]) >= 0)
                SiftDown(index);
            else
                SiftUp(index);

            return true;
        }

        public bool Contains(T item)
        {
            return map.ContainsKey(item);
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

                if (left < count && priorityComparer(priorities[left], priorities[smallest]) < 0)
                    smallest = left;
                if (right < count && priorityComparer(priorities[right], priorities[smallest]) < 0)
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

            map[data[a]] = a;
            map[data[b]] = b;
        }
    }
}