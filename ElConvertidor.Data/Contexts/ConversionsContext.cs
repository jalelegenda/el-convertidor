using ElConvertidor.Data.Entities;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ElConvertidor.Data.Contexts
{
    public class ConversionsContext : DbContext
    {
        public DbSet<InputImage> InputImage { get; set; }
        public DbSet<Conversion> Conversion { get; set; }

        public ConversionsContext() : base("Conversions")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
