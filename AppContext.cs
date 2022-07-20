using DalistoTask2.Model;
using Microsoft.EntityFrameworkCore;

namespace DalistoTask2
{
    public class AppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = LAPTOP-U88VS3V2\SQLEXPRESS; Initial Catalog = dalistoTask5; Integrated Security = True");
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<TaxOffice> TaxOffices { get; set; }

    }
}
