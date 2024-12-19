using Microsoft.EntityFrameworkCore;

namespace ringApi.Model
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        { }

        public DataContext(DbContextOptions options) : base(options)
        { }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                ("Password=adminsql;Persist Security Info=True;User ID=sa;Initial Catalog=forge;Data Source=MURILOSINNER; TrustServerCertificate=True");
        }


        public DbSet<Ring> Rings { get; set; }
    }
}
