using DirectoryService.Domain.Departments;
using DirectoryService.Domain.ValueObjects;
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
            .HasColumnName("department_id");

        builder.Property(x => x.Name)
            .HasConversion(
                v => v.Value,
                v => NameDepartment.Create(v).Value)
            .HasMaxLength(150)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(x => x.Identifier)
            .HasConversion(
                v => v.Value,
                v => Identifier.Create(v).Value)
            .HasMaxLength(150)
            .HasColumnName("identifier")
            .IsRequired();

        builder.Property(x => x.ParentId)
            .IsRequired(false)
            .HasColumnName("parent_id");

        builder.ComplexProperty(x => x.Path, a =>
        {
            a.Property(x => x.Value)
                .HasColumnName("path")
                .IsRequired();
        });

        builder.Property(x => x.Depth)
            .HasColumnName("depth")
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

        builder
            .HasOne<Department>()
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .HasConstraintName("fk_departments_parent_departments");

        builder.HasIndex(x => x.ParentId)
            .HasDatabaseName("ix_departments_parent_parent_id");
    }
}