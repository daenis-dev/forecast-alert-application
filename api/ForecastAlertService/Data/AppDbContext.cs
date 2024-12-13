using Microsoft.EntityFrameworkCore;
using ForecastAlertService.Entities;

namespace ForecastAlertService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        internal DbSet<Specification> Specifications { get; set; }
        internal DbSet<Operator> Operators { get; set; }
        internal DbSet<Alert> Alerts { get; set; }
        internal DbSet<AlertSpecification> AlertSpecifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specification>().ToTable("specifications");

            modelBuilder.Entity<Specification>(entity =>
            {
                entity.Property(s => s.Id).HasColumnName("id");
                entity.Property(s => s.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Operator>().ToTable("operators");

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.Property(o => o.Id).HasColumnName("id");
                entity.Property(o => o.Name).HasColumnName("name");
                entity.Property(o => o.Symbol).HasColumnName("symbol");
            });

            modelBuilder.Entity<Alert>().ToTable("alerts");

            modelBuilder.Entity<Alert>(entity =>
            {
                entity.Property(a => a.Id).HasColumnName("id");
                entity.Property(a => a.Name).HasColumnName("name");
                entity.Property(a => a.IsUrgent).HasColumnName("is_urgent");
                entity.Property(a => a.CreatedDateTimeUtc).HasColumnName("created_datetime_utc");
                entity.Property(a => a.ModifiedDateTimeUtc).HasColumnName("modified_datetime_utc");
            });

            modelBuilder.Entity<AlertSpecification>().ToTable("alert_specifications");

            modelBuilder.Entity<AlertSpecification>(entity =>
            {
                entity.Property(aspec => aspec.Id).HasColumnName("id");
                entity.Property(aspec => aspec.AlertId).HasColumnName("alert_id");
                entity.Property(aspec => aspec.SpecificationId).HasColumnName("specification_id");
                entity.Property(aspec => aspec.OperatorId).HasColumnName("operator_id");
                entity.Property(aspec => aspec.ThresholdValue).HasColumnName("threshold_value");
                entity.Property(aspec => aspec.CreatedDateTimeUtc).HasColumnName("created_datetime_utc");
                entity.Property(aspec => aspec.ModifiedDateTimeUtc).HasColumnName("modified_datetime_utc");

                entity.HasOne(aspec => aspec.Alert)
                    .WithMany(a => a.AlertSpecifications)
                    .HasForeignKey(aspec => aspec.AlertId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(aspec => aspec.Specification)
                    .WithMany()
                    .HasForeignKey(aspec => aspec.SpecificationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(aspec => aspec.Operator)
                    .WithMany()
                    .HasForeignKey(aspec => aspec.OperatorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
