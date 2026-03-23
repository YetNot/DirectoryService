using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("department_positions");

        builder.HasKey(x => x.DepartmentPositionId)
            .HasName("pk_department_positions");

        builder.Property(x => x.DepartmentPositionId)
            .HasColumnName("department_position_id");

        builder.Property(x => x.DepartmentId)
            .HasColumnName("department_id");

        builder.Property(x => x.PositionId)
            .HasColumnName("position_id");

        builder
            .HasOne<Department>()
            .WithMany(p => p.DepartmentPositions)
            .HasForeignKey(d => d.DepartmentId)
            .HasConstraintName("fk_department_positions_departments")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<Position>()
            .WithMany(p => p.DepartmentPositions)
            .HasForeignKey(d => d.PositionId)
            .HasConstraintName("fk_department_positions_positions")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.PositionId, x.DepartmentId })
            .IsUnique()
            .HasDatabaseName("ux_department_positions_position_id_department_id");
    }
}