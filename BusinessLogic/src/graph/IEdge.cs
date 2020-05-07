namespace BusinessLogic.src.graph
{
    internal interface IEdge
    {
        double? Weight { get; set; }
        Edge<X,Y> GetSelfTyped<X,Y>();
        INode this[char letter] { get; }
        Node<H> GetNode<H>(string key);
        bool HasKeys(string keyOne, string keyTwo);
        bool HasKeys<X, Y>(string keyOne, string keyTwo);
        bool HasKey(string key);
        bool HasKey<H>(string key);
    }
}
