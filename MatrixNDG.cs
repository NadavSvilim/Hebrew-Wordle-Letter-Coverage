class MatrixNDG<T> : NonDirectedGraph<T>{
    private int size;
    private int currentIndex = 0;
    private int edgeCount = 0;
    private Dictionary<T, int> vertexIndices;
    private T[] vertices;
    private int[,] matrix;

    public MatrixNDG(int size){
        this.size = size;
        vertexIndices = new Dictionary<T, int>();
        vertices = new T[size];
        matrix = new int[size, size];
    }

    public void AddVertex(T vertex){
        if (currentIndex < size){
            vertices[currentIndex] = vertex;
            vertexIndices[vertex] = currentIndex;
            currentIndex++;
        }
    }

    public void AddEdge(T source, T destination){
        if (vertexIndices.ContainsKey(source) && vertexIndices.ContainsKey(destination)){
            matrix[vertexIndices[source], vertexIndices[destination]] = 1;
            matrix[vertexIndices[destination], vertexIndices[source]] = 1;
            edgeCount++;
        }
    }

    public void RemoveVertex(T vertex){
        if (vertexIndices.ContainsKey(vertex)){
            int index = vertexIndices[vertex];
            for (int i = 0; i < size; i++){
                matrix[index, i] = 0;
                matrix[i, index] = 0;
            }
            vertexIndices.Remove(vertex);
            vertices[index] = default(T);
            currentIndex--;
        }
    }

    public void RemoveEdge(T source, T destination){
        if (vertexIndices.ContainsKey(source) && vertexIndices.ContainsKey(destination)){
            matrix[vertexIndices[source], vertexIndices[destination]] = 0;
            matrix[vertexIndices[destination], vertexIndices[source]] = 0;
            edgeCount--;
        }
    }

    public bool HasVertex(T vertex){
        return vertexIndices.ContainsKey(vertex);
    }

    public bool HasEdge(T source, T destination){
        if (vertexIndices.ContainsKey(source) && vertexIndices.ContainsKey(destination)){
            return matrix[vertexIndices[source], vertexIndices[destination]] == 1;
        }
        return false;
    }

    public int VertexCount(){
        return currentIndex;
    }

    public int EdgeCount(){
        return edgeCount;
    }

    public List<T> Neighbors(T vertex){
        List<T> neighbors = new List<T>();
        if (vertexIndices.ContainsKey(vertex)){
            int index = vertexIndices[vertex];
            for (int i = 0; i < size; i++){
                if (matrix[index, i] == 1){
                    neighbors.Add(vertices[i]);
                }
            }
        }
        return neighbors;
    }

    public List<T> Vertices(){
        return vertices.ToList();
    }

    public override string ToString(){
        string output = "";
        for (int i = 0; i < size; i++){
            for (int j = 0; j < size; j++){
                output += matrix[i, j] + " ";
            }
            output += "\n";
        }
        return output;
    }

    public void Print(){
        Console.WriteLine(ToString());
    }

    public void Clear(){
        vertexIndices.Clear();
        vertices = new T[size];
        matrix = new int[size, size];
        currentIndex = 0;
        edgeCount = 0;
    }

    public List<Tuple<T, T>> Edges(){
        List<Tuple<T, T>> edges = new List<Tuple<T, T>>();
        for (int i = 0; i < size; i++){
            for (int j = i; j < size; j++){
                if (matrix[i, j] == 1){
                    edges.Add(new Tuple<T, T>(vertices[i], vertices[j]));
                }
            }
        }
        return edges;
    }
}