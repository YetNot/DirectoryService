using DirectoryService.Domain.DepartmentPositions;
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

        builder.HasKey(x => x.Id)
            .HasName("pk_department_positions");

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("department_position_id")
            .HasConversion(
                value => value.Value,
                value => new DepartmentPositionId(value));

        builder.Property(x => x.DepartmentId)
            .IsRequired()
            .HasColumnName("department_id")
            .HasConversion(
                value => value.Value,
                value => new DepartmentId(value));

        builder.Property(x => x.PositionId)
            .IsRequired()
            .HasColumnName("position_id")
            .HasConversion(
                value => value.Value,
                value => new PositionId(value));
    }
}