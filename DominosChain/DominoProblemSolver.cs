namespace DominosChain;

public class DominoProblemSolver
{
    // Let's assume each domino value is a graph node
    // Then 2 nodes are connected with edges if the corresponding domino exist
    
    // List of domino stones
    private List<DominoStone> dominoStones;
    // List of edges
    private Dictionary<int, List<DominoStone>> edges;
    // Degree of graph nodes
    private Dictionary<int, int> degree;
    
    public string FailureReason { get; private set; }
    public bool IsCircularChainPossible()
    {
        foreach (var deg in degree)
        {
            if (deg.Value % 2 != 0)
            {
                throw new DominoProblemException( $"Value {deg.Key} occurs {deg.Value} time(s) on the domino stones.");
            }
        }

        return IsGraphConnected();
    }

    public DominoProblemSolver(List<DominoStone> stones)
    {
        dominoStones = new List<DominoStone>();
        edges = new Dictionary<int, List<DominoStone>>();
        degree = new Dictionary<int, int>();
        InitNodesAndEdges(stones);
    }

    public void InitNodesAndEdges(List<DominoStone> stones)
    {
        dominoStones.Clear();
        dominoStones.AddRange(stones);
       
        foreach (var domino in dominoStones)
        {
            if (!edges.ContainsKey(domino.A))
            {
                edges[domino.A] = new List<DominoStone>();
                degree[domino.A] = 0;
            }
            if (!edges.ContainsKey(domino.B))
            {
                edges[domino.B] = new List<DominoStone>();
                degree[domino.B] = 0;
            }
            
            edges[domino.A].Add(domino);
            degree[domino.A]++;

            edges[domino.B].Add(domino);
            degree[domino.B]++;
        }
    }

    /// <summary>
    /// Builds the circular chain if it can be built. If chain is impossible to be built null is returned or DominoProblemException is thrown
    /// </summary>
    /// <exception cref="DominoProblemException">Is thrown when number of values on the dominos set is not even.</exception>
    /// <returns>Domino stones sequence which forms circular chain</returns>
    public List<DominoStone>? Solve()
    {
        return FindEulerianCycle();
    }
    
    private List<DominoStone>? FindEulerianCycle()
    {
        if (!IsCircularChainPossible())
            return null;

        // Hierholzer's algorithm to find an Eulerian cycle
        Stack<DominoStone?> currentPath = new Stack<DominoStone?>();
        List<DominoStone> eulerianCycle = new List<DominoStone>();

        int currentNode = dominoStones[0].A;
        currentPath.Push(null);  // Dummy initial domino

        while (currentPath.Count > 0)
        {
            if (edges[currentNode].Count > 0)
            {
                var nextDomino = edges[currentNode][0];
                currentPath.Push(nextDomino);

                // Remove the domino from the edges list for both nodes
                edges[currentNode].Remove(nextDomino);
                edges[nextDomino.A == currentNode ? nextDomino.B : nextDomino.A].Remove(nextDomino);

                // Move to the next nodes
                currentNode = nextDomino.A == currentNode ? nextDomino.B : nextDomino.A;
            }
            else
            {
                // Backtrack in the path and record the cycle
                var domino = currentPath.Pop();
                if (domino != null)
                {
                    eulerianCycle.Add(domino);
                    currentNode = domino.A == currentNode ? domino.B : domino.A;
                }
            }
        }

        return eulerianCycle;
    }

    private bool IsGraphConnected()
    {
        HashSet<int> visited = new HashSet<int>();
        Queue<int> queue = new Queue<int>();
        
        // Start BFS from any node
        queue.Enqueue(edges.Keys.First());
        visited.Add(edges.Keys.First());

        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            foreach (var neighbour in edges[node])
            {
                int nextNode = neighbour.A == node ? neighbour.B : neighbour.A;
                if (!visited.Contains(nextNode))
                {
                    visited.Add(nextNode);
                    queue.Enqueue(nextNode);
                }
            }
        }

        // Check if all nodes were visited
        foreach (var node in edges.Keys)
        {
            if (!visited.Contains(node))
                return false;  // Not all nodes were reachable
        }
        return true;
    }
}