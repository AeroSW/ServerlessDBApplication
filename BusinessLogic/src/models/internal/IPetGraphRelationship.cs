using BusinessLogic.src.graph;

namespace BusinessLogic.src.models
{
    internal abstract class IPetGraphRelationship
    {
        public string Type { get; set; }
        protected IGraph Graph { get; set; }
    }
}
