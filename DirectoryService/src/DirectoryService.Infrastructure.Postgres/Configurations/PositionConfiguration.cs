using DirectoryService.Domain.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");

        builder.HasKey(x => x.Id)
            .HasName("pk_positions");

        builder.Property(x => x.Id)
            .HasColumnName("position_id");

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasColumnName("description");

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