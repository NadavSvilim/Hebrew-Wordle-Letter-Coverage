class AdjListNDG<T> : NonDirectedGraph<T> where T : notnull {
    public Dictionary<T, List<T>> adjList { get; private set; }
    private int edgeCount;

    public AdjListNDG() {
        adjList = new Dictionary<T, List<T>>();
        edgeCount = 0;
    }

    public void AddVertex(T vertex) {
        if (!HasVertex(vertex)) {
            adjList[vertex] = new List<T>();
        }
    }

    public void AddEdge(T source, T destination) {
        if (HasVertex(source) && HasVertex(destination) && !HasEdge(source, destination)) {
            adjList[source].Add(destination);
            adjList[destination].Add(source);
            edgeCount++;
        }
    }

    public void RemoveVertex(T vertex) {
        if (HasVertex(vertex)) {
            adjList.Remove(vertex);
            foreach (var adj in adjList) {
                adj.Value.Remove(vertex);
            }
        }
    }

    public void RemoveEdge(T source, T destination) {
        if (HasEdge(source, destination)) {
            adjList[source].Remove(destination);
            adjList[destination].Remove(source);
        }
    }

    public bool HasVertex(T vertex) {
        return adjList.ContainsKey(vertex);
    }

    public bool HasEdge(T source, T destination) {
        return adjList.ContainsKey(source) && adjList[source].Contains(destination);
    }

    public int VertexCount() {
        return adjList.Count;
    }

    public int EdgeCount() {
        return edgeCount;
    }

    public List<T> Neighbors(T vertex) {
        return adjList[vertex];
    }

    public List<T> Vertices() {
        return adjList.Keys.ToList();
    }

    public List<Tuple<T, T>> Edges() {
        List<Tuple<T, T>> edges = new List<Tuple<T, T>>();
        foreach (var vertex in adjList) {
            foreach (var neighbor in vertex.Value) {
                edges.Add(new Tuple<T, T>(vertex.Key, neighbor));
            }
        }
        return edges;
    }

    public void Clear() {
        adjList.Clear();
        edgeCount = 0;
    }

    public void Print() {
        foreach (var vertex in adjList) {
            Console.Write(vertex.Key + ": ");
            foreach (var neighbor in vertex.Value) {
                Console.Write(neighbor + " ");
            }
            Console.WriteLine();
        }
    }

} 