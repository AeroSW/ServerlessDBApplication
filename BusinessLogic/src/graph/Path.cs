using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.graph
{
    public class Path
    {
        public double Distance { get; }
        public List<INode> Nodes { get; }
        public Path(double distance, List<INode> nodes)
        {
            this.Distance = distance;
            this.Nodes = nodes;
        }
    }
}
