using DirectoryService.Domain.Locations;
using DirectoryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeZone = DirectoryService.Domain.ValueObjects.TimeZone;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.HasKey(x => x.Id)
            .HasName("pk_locations");

        builder.Property(x => x.Id)
            .HasColumnName("location_id");

        builder.Property(x => x.Name)
            .HasConversion(
                v => v.Value,
                v => NameLocation.Create(v).Value)
            .HasMaxLength(150)
            .HasColumnName("name")
            .IsRequired();

        builder.ComplexProperty(x => x.Address, a =>
        {
            a.Property(x => x.Country)
                .HasColumnName("country");

            a.Property(x => x.City)
                .HasColumnName("city");

            a.Property(x => x.Street)
                .HasColumnName("street");

            a.Property(x => x.HouseNumber)
                .HasColumnName("house_number");
        });

        builder.Property(x => x.TimeZone)
            .HasConversion(
                v => v.Value,
                v => TimeZone.Create(v).Value)
            .HasMaxLength(100)
            .HasColumnName("time_zone")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("now()")
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasDefaultValueSql("now()")
            .HasColumnName("updated_at")
            .IsRequired();
    }
}