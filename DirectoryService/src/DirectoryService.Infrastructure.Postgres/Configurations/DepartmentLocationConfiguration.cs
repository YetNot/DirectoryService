using DirectoryService.Domain.DepartmentLocations;
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

        builder.HasKey(x => x.Id)
            .HasName("pk_department_locations");

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("department_location_id")
            .HasConversion(
                value => value.Value,
                value => new DepartmentLocationId(value));

        builder.Property(x => x.DepartmentId)
            .IsRequired()
            .HasColumnName("department_id")
            .HasConversion(
                value => value.Value,
                value => new DepartmentId(value));

        builder.Property(x => x.LocationId)
            .IsRequired()
            .HasColumnName("location_id")
            .HasConversion(
                value => value.Value,
                value => new LocationId(value));
    }
}