using BusinessLogic.src.graph;
using BusinessLogic.src.models;
using System;

namespace BusinessLogic.src.repositories.pet_repository
{
    public partial class PetRepository
    {
        public InsertSuccess AddDog(Dog dog)
        {
            var uniqueKey = Guid.NewGuid().ToString();
            var dogNode = new Node<Dog>(uniqueKey, dog);
            var result = this._Graph.AddNode(dogNode);
            if (result)
            {
                createDogNodes(dog, dogNode);
            }
            return new InsertSuccess
            {
                success = result,
                key = uniqueKey
            };
        }
        private void createDogNodes(Dog dog, Node<Dog> dogNode)
        {
            Node<string> dogName;
            Node<string> name = this.GetStringNode("name", dog.name, out dogName);
            this.buildRelationship<Dog,string>(dogNode, dogName);
            Node<string> dogAddress;
            Node<string> address = this.GetStringNode("address", dog.name, out dogAddress);
            this.buildRelationship<Dog,string>(dogNode, dogAddress);
            Node<string> dogOwner;
            Node<string> owner = this.GetStringNode("owner", dog.name, out dogOwner);
            this.buildRelationship<Dog,string>(dogNode, dogOwner);
            Node<string> dogBreed;
            Node<string> breed = this.GetStringNode("breed", dog.name, out dogBreed);
            this.buildRelationship<Dog,string>(dogNode, dogBreed);
            Node<string> dogBreeder;
            Node<string> breeder = this.GetStringNode("breeder", dog.name, out dogBreeder);
            this.buildRelationship<Dog,string>(dogNode, dogBreeder);
            Node<bool> outdoor = this.GetBoolNode(dog.outdoor, "outdoor", "indoor");
            this.buildRelationship<Dog,bool>(dogNode, outdoor);
            Node<bool> attack = this.GetBoolNode(dog.attack, "attack", "playful");
            this.buildRelationship<Dog,bool>(dogNode, attack);
            Node<bool> service = this.GetBoolNode(dog.service, "service", "normal");
            this.buildRelationship<Dog,bool>(dogNode, service);
        }
    }
}
