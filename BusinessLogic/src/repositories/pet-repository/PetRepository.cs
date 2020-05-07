using BusinessLogic.src.graph;
using BusinessLogic.src.models;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace BusinessLogic.src.repositories.pet_repository
{
    public partial class PetRepository: IPetRepository
    {
        private Graph _Graph { get; set; }
        public PetRepository()
        {
            this._Graph = new Graph();
        }
        private Node<bool> GetBoolNode(bool test, string passKey, string failKey)
        {
            Node<bool> node = (test) ? this._Graph.GetNode<bool>(passKey) : this._Graph.GetNode<bool>(failKey);
            if(node == null)
            {
                var key = (test) ? passKey : failKey;
                node = new Node<bool>(key, test);
                this._Graph.AddNode<bool>(node);
            }
            return node;
        }
        private Node<string> GetStringNode(string key, string name, out Node<string> namedNode)
        {
            Node<string> keyNode = null;
            if (!this._Graph.HasKey<string>(key))
            {
                keyNode = new Node<string>(key, key);
                this._Graph.AddNode(keyNode);
            }
            else
            {
                keyNode = this._Graph.GetNode<string>(key);
            }
            if (!this._Graph.HasKey<string>(name))
            {
                Node<string> tNode = new Node<string>(name, name);
                this._Graph.AddNode(tNode);
                namedNode = tNode;
            }
            else
            {
                namedNode = this._Graph.GetNode<string>(name);
            }
            if(!this._Graph.HasEdge<string, string>(key, name))
            {
                this._Graph.AddEdge<string, string>(key, name);
            }
            return keyNode;
        }
        private void buildRelationship<T, G>(Node<T> pet, Node<G> gNode)
        {
            if (!this._Graph.HasEdge<T, G>(pet.Key, gNode.Key))
            {
                this._Graph.AddEdge<T, G>(pet.Key, gNode.Key);
            }
        }
    }
}
