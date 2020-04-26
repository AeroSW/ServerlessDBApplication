using BusinessLogic.src.graph;
using BusinessLogic.src.models;
using System.Collections.Generic;

namespace BusinessLogic.src.repositories.pet_repository
{
    public partial class PetRepository: IPetRepository
    {
        private Dictionary<string, IPetGraphRelationship> graphs;
        public PetRepository()
        {
            graphs = new Dictionary<string, IPetGraphRelationship>();
        }
    }
}
