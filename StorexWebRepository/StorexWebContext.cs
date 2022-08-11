using Microsoft.EntityFrameworkCore;
using StorexWebCore.Enities;


namespace StorexWebRepository
{
    public class StorexWebContext : CustomContext
    {
        private string _conn;
        public StorexWebContext(string connectionString) : base()
        {
            // Default Constructor
            _conn = connectionString;
        }

        public StorexWebContext(DbContextOptions optionsBuilder, string connectionString) : base(optionsBuilder, false)
        {
            _conn = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conn);
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<CurrentUser> CurrentUser { get; set; }

        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }


    }
}
