using GameStore.StoreDomain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GameStore.StoreDomain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("EFDbConnection")
        { }

        DbSet<Admin> Admins { get; set; }
        DbSet<User> Users { get; set; } 
        DbSet<Comment> Comments { get; set; } 
        DbSet<Game> Games { get; set; }
        DbSet<GameAddition> GameAdditions { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<UserCabinet> UserCabinets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
