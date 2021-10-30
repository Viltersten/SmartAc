using Api.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Auxiliaries
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Junk> Junks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            OnModelCreating(builder.Entity<Device>());
            OnModelCreating(builder.Entity<Measure>());
            OnModelCreating(builder.Entity<Alert>());
            OnModelCreating(builder.Entity<Junk>());
        }

        private static void OnModelCreating(EntityTypeBuilder<Device> entity)
        {
            entity.Ignore(a => a.Version);
        }

        private static void OnModelCreating(EntityTypeBuilder<Measure> entity)
        {
            entity.HasKey(a => new { a.DeviceId, a.RecordedOn });
        }

        private static void OnModelCreating(EntityTypeBuilder<Alert> entity)
        {
            entity.HasOne(a => a.Measure);
        }

        private static void OnModelCreating(EntityTypeBuilder<Junk> entity)
        {
            entity.Property(a => a.Payload).HasMaxLength(500);
        }
    }
}
