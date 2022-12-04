using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoOrganizer.Backend.Domain.Aggregates;

namespace ToDoOrganizer.Backend.Infrastructure.DAL.Configurations;

internal sealed class ProjectConfiguration : EntityConfigurationBase<Project>
{
    public override void Configure(EntityTypeBuilder<Project> builder)
    {
        base.Configure(builder);

        builder.HasMany(p => p.ToDoItems)
            .WithOne(toDoEntity => toDoEntity.Project)
            .HasForeignKey(p => p.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(1000);
    }
}