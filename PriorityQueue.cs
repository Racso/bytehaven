namespace rac.so.bytehaven
{
    public class PriorityQueue<T>
    {
        private readonly BinaryHeapWithMap<T> heap;

        public PriorityQueue(int capacity, bool isMaxQueue = false)
        {
            Func<int, int, int> comparer = isMaxQueue ? (a, b) => b - a : (a, b) => a - b;
            heap = new BinaryHeapWithMap<T>(capacity, comparer);
        }

        public void Enqueue(int priority, T value)
        {
            heap.Push(value, priority);
        }

        public (T Value, int priority) Dequeue()
        {
            return heap.Pop();
        }

        public (T Value, int priority) Peek()
        {
            return heap.Peek();
        }

        public bool Remove(T item)
        {
            return heap.Remove(item);
        }

        public void SetPriority(T item, int priority)
        {
            Remove(item);
            Enqueue(priority, item);
        }

        public bool Contains(T item)
        {
            return heap.Contains(item);
        }

        public int Count => heap.Count;
    }
}