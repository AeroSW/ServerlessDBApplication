using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.graph
{
    public interface IGraph
    {
        Graph<T> get<T>();
    }
}
