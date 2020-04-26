using BusinessLogic.src.graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.models
{
    internal class PetGraphRelationship<T> : IPetGraphRelationship
    {
        public Graph<T> GetGraph()
        {
            return Graph.get<T>();
        }

        internal PetGraphRelationship(Graph<T> graph)
        {
            this.Type = typeof(T).ToString();
            this.Graph = graph;
        }
    }
}
