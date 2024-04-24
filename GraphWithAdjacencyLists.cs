public class GraphWithAdjacencyLists
{
    /* An implementation of the Graph data structure. MIT License.
   Made with ♠ by Racso. https://rac.so | https://github.com/Racso */

    private readonly int initialCapacity;
    private readonly Dictionary<int, List<int>> adj;

    public GraphWithAdjacencyLists(int initialCapacity)
    {
        this.initialCapacity = initialCapacity;
        adj = new Dictionary<int, List<int>>(initialCapacity);
    }

    public void AddEdge(int nodeA, int nodeB)
    {
        if (!adj.ContainsKey(nodeA))
            adj[nodeA] = new List<int>(initialCapacity);

        if (!adj.ContainsKey(nodeB))
            adj[nodeB] = new List<int>(initialCapacity);

        adj[nodeA].Add(nodeB);
        adj[nodeB].Add(nodeA);
    }

    public void RemoveEdge(int nodeA, int nodeB)
    {
        if (!adj.ContainsKey(nodeA) || !adj.ContainsKey(nodeB))
            return;

        adj[nodeA].Remove(nodeB);
        adj[nodeB].Remove(nodeA);
    }

    public void RemoveNode(int node)
    {
        if (!adj.ContainsKey(node))
            return;

        foreach (int neighbor in adj[node])
            adj[neighbor].Remove(node);

        adj.Remove(node);
    }

    public int GetDegree(int node)
        => adj.ContainsKey(node) ? adj[node].Count : 0;

    public IEnumerable<int> GetNeighbors(int node)
        => adj.ContainsKey(node) ? adj[node] : Array.Empty<int>();

    public IEnumerable<int> GetNodes()
        => adj.Keys;

    public int GetNodeCount()
        => adj.Count;
}