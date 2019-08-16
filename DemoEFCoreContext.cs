using Microsoft.EntityFrameworkCore;

namespace DemoEFCore
{
    public class DemoEFCoreContext : DbContext
    {
        public DemoEFCoreContext(DbContextOptions<DemoEFCoreContext> options) : base(options) { }

        public DbSet<TableMaster> TableMasters { get; set; }
        public DbSet<TableDetail> TableDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TableMasterConfiguration());
            modelBuilder.ApplyConfiguration(new TableDetailConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}