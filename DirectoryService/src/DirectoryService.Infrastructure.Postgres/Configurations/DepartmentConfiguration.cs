using DirectoryService.Domain.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasKey(x => x.Id)
            .HasName("pk_departments");

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("id")
            .HasConversion(
                value => value.Value,
                value => new DepartmentId(value));

        builder.ComplexProperty(x => x.Name, nb =>
        {
            nb.Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(DepartmentName.NAME_MAX_LENGTH)
                .IsRequired();
        });

        builder.ComplexProperty(x => x.Identifier, nb =>
        {
            nb.Property(n => n.Value)
                .HasColumnName("identifier")
                .HasMaxLength(Identifier.IDENTIFIER_MAX_LENGTH)
                .IsRequired();
        });

        builder.Property(x => x.ParentId)
            .IsRequired(false)
            .HasColumnName("parent_id")
            .HasConversion(
                value => value!.Value,
                value => new DepartmentId(value));

        builder.ComplexProperty(x => x.Path, nb =>
        {
            nb.Property(n => n.Value)
                .HasColumnName("path")
                .IsRequired();
        });

        builder.Property(x => x.Depth)
            .IsRequired()
            .HasColumnName("depth");

        builder.Property(x => x.ChildrenCount)
            .IsRequired()
            .HasColumnName("children_count");

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasColumnName("is_active");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        builder.HasMany(x => x.ChildrenDepartments)
            .WithOne()
            .IsRequired(false)
            .HasForeignKey(x => x.ParentId)
            .HasConstraintName("fk_children_departments")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.DepartmentLocations)
            .WithOne()
            .HasForeignKey(x => x.DepartmentId)
            .HasConstraintName("fk_department_locations");

        builder.HasMany(x => x.DepartmentPositions)
            .WithOne()
            .HasForeignKey(x => x.DepartmentId)
            .HasConstraintName("fk_department_positions");
    }
}