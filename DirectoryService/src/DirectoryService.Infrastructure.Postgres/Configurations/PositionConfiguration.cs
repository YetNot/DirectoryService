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
            .IsRequired()
            .HasColumnName("position_id")
            .HasConversion(
                value => value.Value,
                value => new PositionId(value));

        builder.ComplexProperty(x => x.Name, nb =>
        {
            nb.Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(PositionName.NAME_MAX_LENGTH)
                .IsRequired();
        });

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasColumnName("description")
            .HasMaxLength(Description.DESCRIPTION_MAX_LENGTH)
            .HasConversion(
                value => value!.Value,
                value => new Description(value));

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasColumnName("is_active");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        builder.HasMany(x => x.DepartmentPositions)
            .WithOne()
            .HasForeignKey(x => x.PositionId)
            .HasConstraintName("fk_positions_departments");
    }
}