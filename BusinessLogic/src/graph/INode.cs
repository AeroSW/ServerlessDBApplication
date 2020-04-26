using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.graph
{
    public interface INode: IComparable<INode>
    {
        string key { get; }
    }
}
