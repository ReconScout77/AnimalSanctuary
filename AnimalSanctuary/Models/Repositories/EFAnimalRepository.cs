using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalSanctuary.Models
{
    public class EFAnimalRepository : IAnimalRepository


    {

        public EFAnimalRepository(AnimalSanctuaryContext connection = null)
        {
            if(connection == null)
            {
                this.db = new AnimalSanctuaryContext();
            }
            else{
                this.db = connection;
            }
        }

        AnimalSanctuaryContext db = new AnimalSanctuaryContext();

        public IQueryable<Animal> Animals
        { get { return db.Animals; } }

        public Animal Save(Animal animal)
        {
            db.Animals.Add(animal);
            db.SaveChanges();
            return animal;
        }

        public Animal Edit(Animal animal)
        {
            db.Entry(animal).State = EntityState.Modified;
            db.SaveChanges();
            return animal;
        }

        public void Remove(Animal animal)
        {
            db.Animals.Remove(animal);
            db.SaveChanges();
        }
    }
}