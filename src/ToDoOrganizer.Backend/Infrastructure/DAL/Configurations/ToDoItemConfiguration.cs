using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoOrganizer.Backend.Domain.Entities;

namespace ToDoOrganizer.Backend.Infrastructure.DAL.Configurations;

internal sealed class ToDoItemConfiguration : EntityConfigurationBase<ToDoItem>
{
    public override void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        base.Configure(builder);

        builder.HasOne(p => p.Project)
            .WithMany(p => p.ToDoItems)
            .HasForeignKey(p => p.ProjectId);

        builder.Property(p => p.IsCompleted).IsRequired().HasDefaultValue(false);
        builder.Property(p => p.Name).IsRequired();
    }
}