using DirectoryService.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeZone = DirectoryService.Domain.Locations.TimeZone;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.HasKey(x => x.Id)
            .HasName("pk_locations");

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("location_id")
            .HasConversion(
                value => value.Value,
                value => new LocationId(value));

        builder.ComplexProperty(x => x.Name, nb =>
        {
            nb.Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(LocationName.LOCATION_MAX_LENGTH)
                .IsRequired();
        });

        builder.ComplexProperty(x => x.Address, nb =>
        {
            nb.Property(x => x.Country)
                .HasColumnName("country")
                .IsRequired();

            nb.Property(x => x.City)
                .HasColumnName("city")
                .IsRequired();

            nb.Property(x => x.Street)
                .HasColumnName("street")
                .IsRequired();

            nb.Property(x => x.HouseNumber)
                .HasColumnName("house_number")
                .IsRequired();
        });

        builder.ComplexProperty(x => x.TimeZone, nb =>
        {
            nb.Property(n => n.Value)
                .HasColumnName("timezone")
                .IsRequired();
        });

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasColumnName("is_active");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        builder.HasMany(x => x.DepartmentLocations)
            .WithOne()
            .HasForeignKey(x => x.LocationId)
            .HasConstraintName("fk_locations_departments");
    }
}