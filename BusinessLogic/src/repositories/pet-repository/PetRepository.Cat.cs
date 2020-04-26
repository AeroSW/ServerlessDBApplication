using BusinessLogic.src.graph;
using BusinessLogic.src.models;
using System;

namespace BusinessLogic.src.repositories.pet_repository
{
    public partial class PetRepository
    {
        private static string CatKey = typeof(Cat).ToString();
        public InsertSuccess AddCat(Cat cat)
        {
            if (!HasCatGraph())
            {
                CreateCatGraph();
            }
            Graph<Cat> graph = ((PetGraphRelationship<Cat>)this.graphs[PetRepository.CatKey]).GetGraph();
            var uniqueKey = Guid.NewGuid().ToString();
            var catNode = new Node<Cat>(uniqueKey, cat);
            var result = graph.AddNode(catNode);
            return new InsertSuccess
            {
                success = result,
                key = uniqueKey
            };
        }
        private bool HasCatGraph()
        {
            return this.graphs.ContainsKey(PetRepository.CatKey);
        }
        private void CreateCatGraph()
        {
            Graph<Cat> graph = new Graph<Cat>();
            PetGraphRelationship<Cat> petGraphRelationship = new PetGraphRelationship<Cat>(graph);
            this.graphs.Add(petGraphRelationship.Type, petGraphRelationship);
        }
    }
}
