public class GraphWithAdjacencyLists
{
    /* An implementation of the Graph data structure. MIT License.
   Made with ♠ by Racso. https://rac.so | https://github.com/Racso */

    private readonly int initialNeighborsCount;
    private readonly Dictionary<int, List<int>> neighbors;

    public GraphWithAdjacencyLists(int initialNodesCount, int initialNeighborsCount)
    {
        neighbors = new Dictionary<int, List<int>>(initialNodesCount);
        this.initialNeighborsCount = initialNeighborsCount;
    }

    public void AddEdge(int nodeA, int nodeB)
    {
        if (!neighbors.ContainsKey(nodeA))
            neighbors[nodeA] = new List<int>(initialNeighborsCount);

        if (!neighbors.ContainsKey(nodeB))
            neighbors[nodeB] = new List<int>(initialNeighborsCount);

        neighbors[nodeA].Add(nodeB);
        neighbors[nodeB].Add(nodeA);
    }

    public void RemoveEdge(int nodeA, int nodeB)
    {
        if (!neighbors.ContainsKey(nodeA) || !neighbors.ContainsKey(nodeB))
            return;

        neighbors[nodeA].Remove(nodeB);
        neighbors[nodeB].Remove(nodeA);
    }

    public void RemoveNode(int node)
    {
        if (!neighbors.ContainsKey(node))
            return;

        foreach (int neighbor in neighbors[node])
            neighbors[neighbor].Remove(node);

        neighbors.Remove(node);
    }

    public int GetDegree(int node)
        => neighbors.ContainsKey(node) ? neighbors[node].Count : 0;

    public IEnumerable<int> GetNeighbors(int node)
        => neighbors.ContainsKey(node) ? neighbors[node] : Array.Empty<int>();

    public IEnumerable<int> GetNodes()
        => neighbors.Keys;

    public int GetNodeCount()
        => neighbors.Count;
}