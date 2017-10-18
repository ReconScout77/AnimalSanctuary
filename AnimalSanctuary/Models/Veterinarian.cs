using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalSanctuary.Models
{
    [Table("Veterinarians")]
    public class Veterinarian
    {
        [Key]
        public int VeterinarianId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }

        public override bool Equals(System.Object otherVeterinarian)
        {
            if (!(otherVeterinarian is Veterinarian))
            {
                return false;
            }
            else
            {
                Veterinarian newVeterinarian = (Veterinarian)otherVeterinarian;
                return this.VeterinarianId.Equals(newVeterinarian.VeterinarianId);
            }
        }

        public override int GetHashCode()
        {
            return this.VeterinarianId.GetHashCode();
        }
    }

   
}

