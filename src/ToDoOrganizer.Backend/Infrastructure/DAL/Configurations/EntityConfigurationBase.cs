using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoOrganizer.Backend.Domain.Models;

namespace ToDoOrganizer.Backend.Infrastructure.DAL.Configurations;

internal abstract class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasQueryFilter(entity => entity.DeleteDate == null);

        builder.Property(p => p.CreatedBy).IsRequired();

        builder.Property(p => p.CreatedDate)
            .IsRequired()
            .HasConversion(
                toProvider => toProvider.ToUniversalTime(),
                fromProvider => new DateTime(fromProvider.Ticks, DateTimeKind.Utc));

        builder.Property(p => p.UpdateDate)
            .HasConversion(
                toProvider => toProvider!.Value.ToUniversalTime(),
                fromProvider => new DateTime(fromProvider.Ticks, DateTimeKind.Utc));

        builder.Property(p => p.DeleteDate)
            .HasConversion(
                toProvider => toProvider!.Value.ToUniversalTime(),
                fromProvider => new DateTime(fromProvider.Ticks, DateTimeKind.Utc));
    }
}