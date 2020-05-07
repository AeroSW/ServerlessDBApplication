using BusinessLogic.src.graph;
using BusinessLogic.src.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.repositories.pet_repository
{
    public interface IPetRepository
    {
        InsertSuccess AddDog(Dog dog);
        InsertSuccess AddCat(Cat cat);
    }
}
