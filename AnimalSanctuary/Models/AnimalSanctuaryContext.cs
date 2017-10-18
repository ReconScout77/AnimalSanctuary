
using Microsoft.EntityFrameworkCore;


namespace AnimalSanctuary.Models
{
    public class AnimalSanctuaryContext : DbContext
    {
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Veterinarian> Veterinarians { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .UseMySql(@"Server=localhost;Port=8889;database=animal_sanctuary;uid=root;pwd=root;");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
