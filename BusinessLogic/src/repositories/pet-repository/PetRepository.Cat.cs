using BusinessLogic.src.graph;
using BusinessLogic.src.models;
using System;

namespace BusinessLogic.src.repositories.pet_repository
{
    public partial class PetRepository
    {
        public InsertSuccess AddCat(Cat cat)
        {
            var uniqueKey = Guid.NewGuid().ToString();
            var catNode = new Node<Cat>(uniqueKey, cat);
            var result = this._Graph.AddNode(catNode);
            if (result)
            {
                createCatNodes(cat, catNode);
            }
            return new InsertSuccess
            {
                success = result,
                key = uniqueKey
            };
        }
        private void createCatNodes(Cat cat, Node<Cat> catNode)
        {
            Node<string> catName;
            Node<string> name = this.GetStringNode("name", cat.name, out catName);
            this.buildRelationship<Cat, string>(catNode, catName);
            Node<string> catAddress;
            Node<string> address = this.GetStringNode("address", cat.name, out catAddress);
            this.buildRelationship<Cat, string>(catNode, catAddress);
            Node<string> catOwner;
            Node<string> owner = this.GetStringNode("owner", cat.name, out catOwner);
            this.buildRelationship<Cat, string>(catNode, catOwner);
            Node<string> catBreed;
            Node<string> breed = this.GetStringNode("breed", cat.name, out catBreed);
            this.buildRelationship<Cat, string>(catNode, catBreed);
            Node<string> catBreeder;
            Node<string> breeder = this.GetStringNode("breeder", cat.name, out catBreeder);
            this.buildRelationship<Cat, string>(catNode, catBreeder);
            Node<bool> outdoor = this.GetBoolNode(cat.outdoor, "outdoor", "indoor");
            this.buildRelationship<Cat, bool>(catNode, outdoor);
        }

    }
}
