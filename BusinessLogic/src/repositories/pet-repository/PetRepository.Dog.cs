using BusinessLogic.src.graph;
using BusinessLogic.src.models;
using System;

namespace BusinessLogic.src.repositories.pet_repository
{
    public partial class PetRepository
    {
        private static string DogKey = typeof(Dog).ToString();
        public InsertSuccess AddDog(Dog dog)
        {
            if (!HasDogGraph())
            {
                CreateDogGraph();
            }
            Graph<Dog> graph = ((PetGraphRelationship<Dog>)this.graphs[PetRepository.DogKey]).GetGraph();
            var uniqueKey = Guid.NewGuid().ToString();
            var dogNode = new Node<Dog>(uniqueKey, dog);
            var result = graph.AddNode(dogNode);
            return new InsertSuccess
            {
                success = result,
                key = uniqueKey
            };
        }
        private bool HasDogGraph()
        {
            return this.graphs.ContainsKey(PetRepository.DogKey);
        }
        private void CreateDogGraph()
        {
            Graph<Dog> graph = new Graph<Dog>();
            PetGraphRelationship<Dog> petGraphRelationship = new PetGraphRelationship<Dog>(graph);
            this.graphs.Add(petGraphRelationship.Type, petGraphRelationship);
        }
    }
}
