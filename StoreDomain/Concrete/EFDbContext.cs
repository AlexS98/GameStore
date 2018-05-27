using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StoreDomain.Concrete
{
    class EFDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
