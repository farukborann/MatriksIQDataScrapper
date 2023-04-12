using Microsoft.EntityFrameworkCore;

namespace MatriksDataScrapper.Database
{
    class Context : DbContext
    {
        public static Context Init = new Context();
        public DbSet<Tick> Ticks { get; set; }
        public Context() 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=matriks;Trusted_Connection=True;TrustServerCertificate=True");
        }

    }
}
