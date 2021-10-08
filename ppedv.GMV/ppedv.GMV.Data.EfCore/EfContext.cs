using Microsoft.EntityFrameworkCore;
using ppedv.GMV.Model;

namespace ppedv.GMV.Data.EfCore
{
    public class EfContext : DbContext
    {
        public DbSet<Gerät> Geräte { get; set; }
        public DbSet<Messwert> Messwerte { get; set; }
        public DbSet<Messung> Messungen { get; set; }


        private string _conString;

        public EfContext(string conString)
        {
            _conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_conString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Gerät>().Ignore(x => x.Wichtig);
        }
    }
}