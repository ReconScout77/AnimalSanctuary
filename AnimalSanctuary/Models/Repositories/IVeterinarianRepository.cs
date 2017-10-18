using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalSanctuary.Models
{
    public interface IVeterinarianRepository
    {
        IQueryable<Veterinarian> Veterinarians { get; }
        Veterinarian Save(Veterinarian veterinarian);
        Veterinarian Edit(Veterinarian veterinarian);
        void Remove(Veterinarian veterinarian);
    }
}