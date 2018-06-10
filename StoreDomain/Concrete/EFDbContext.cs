using StoreDomain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StoreDomain.Concrete
{
    class EFDbContext : DbContext
    {
        public EFDbContext() : base("EFDbConnection")
        { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<Comment> Comments { get; set; } 
        public DbSet<Game> Games { get; set; }
        public DbSet<GameAddition> GameAdditions { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserCabinet> UserCabinets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
