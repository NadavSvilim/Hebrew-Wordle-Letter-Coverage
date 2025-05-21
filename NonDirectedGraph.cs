interface NonDirectedGraph<T>{
    void AddVertex(T vertex);
    void AddEdge(T source, T destination);
    void RemoveVertex(T vertex);
    void RemoveEdge(T source, T destination);
    bool HasVertex(T vertex);
    bool HasEdge(T source, T destination);
    int VertexCount();
    int EdgeCount();
    List<T> Neighbors(T vertex);
    List<T> Vertices();
    List<Tuple<T,T>> Edges();
    void Clear();
    void Print();
}