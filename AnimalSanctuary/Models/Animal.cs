using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace AnimalSanctuary.Models
{
    [Table("Animals")]
    public class Animal
    {
        [Key]
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Sex { get; set; }
        public string HabitatType { get; set; }
        public bool MedicalEmergency { get; set; }
        public int VeterinarianId { get; set; }

        public virtual Veterinarian Veterinarian { get; set; }

        public override bool Equals(System.Object otherAnimal)
        {
            if (!(otherAnimal is Animal))
            {
                return false;
            }
            else
            {
                Animal newAnimal = (Animal)otherAnimal;
                return this.AnimalId.Equals(newAnimal.AnimalId);
            }
        }

        public override int GetHashCode()
        {
            return this.AnimalId.GetHashCode();
        }

     

    }


}

