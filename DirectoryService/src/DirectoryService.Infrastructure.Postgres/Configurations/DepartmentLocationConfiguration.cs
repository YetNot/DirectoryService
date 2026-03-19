using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("department_locations");

        builder.HasKey(x => x.DepartmentLocationId)
            .HasName("pk_department_locations");

        builder.Property(x => x.DepartmentLocationId)
            .HasColumnName("department_location_id");

        builder.Property(x => x.DepartmentId)
            .HasColumnName("department_id");

        builder.Property(x => x.LocationId)
            .HasColumnName("location_id");

        builder
            .HasOne<Department>()
            .WithMany(l => l.DepartmentLocations)
            .HasForeignKey(x => x.DepartmentId)
            .HasConstraintName("fk_department_locations_departments")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<Location>()
            .WithMany(l => l.DepartmentLocations)
            .HasForeignKey(x => x.LocationId)
            .HasConstraintName("fk_department_locations_locations")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.LocationId, x.DepartmentId })
            .IsUnique()
            .HasDatabaseName("ux_department_locations_location_id_department_id");
    }
}