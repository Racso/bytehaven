namespace rac.so.bytehaven
{
    public class UnionFind
    {
        /* An implementation of the Union-Find data structure. MIT License.
           Made with ♠ by Racso. https://rac.so | https://github.com/Racso */

        private readonly int[] parent;
        private readonly int[] rank;

        public UnionFind(int capacity)
        {
            parent = new int[capacity];
            rank = new int[capacity];

            for (int i = 0; i < capacity; i++)
            {
                parent[i] = i;
                rank[i] = 1;
            }
        }

        public int FindSet(int n)
        {
            if (parent[n] == n)
                return n;

            parent[n] = FindSet(parent[n]);
            return parent[n];
        }

        public bool Union(int n, int m)
        {
            int nRoot = FindSet(n);
            int mRoot = FindSet(m);

            if (nRoot == mRoot)
                return false;

            if (rank[nRoot] > rank[mRoot])
                parent[mRoot] = nRoot;
            else if (rank[nRoot] < rank[mRoot])
                parent[nRoot] = mRoot;
            else
            {
                parent[mRoot] = nRoot;
                rank[nRoot]++;
            }

            return true;
        }
    }
}