using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalSanctuary.Models
{
    public interface IAnimalRepository
    {
        IQueryable<Animal> Animals { get; }
        Animal Save(Animal animal);
        Animal Edit(Animal animal);
        void Remove(Animal animal);
    }
}