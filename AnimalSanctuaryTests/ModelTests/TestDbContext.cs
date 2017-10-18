using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuary.Models
{
    public class TestDbContext : AnimalSanctuaryContext
    {
        public override DbSet<Animal> Animals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(@"Server=localhost;Port=8889;database=animal_sanctuary_tests;uid=root;pwd=root;");
        }
    }
}