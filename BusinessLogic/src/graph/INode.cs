using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.graph
{
    public interface INode: IComparable<INode>
    {
        string Key { get; }
        string Type { get; }
        int CompareTo<G>(Node<G> node);
    }
}
