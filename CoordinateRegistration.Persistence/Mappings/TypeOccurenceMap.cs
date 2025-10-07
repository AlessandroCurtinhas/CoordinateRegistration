using CoordinateRegistration.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoordinateRegistration.Persistence.Mappings
{
    public class TypeOccurenceMap : IEntityTypeConfiguration<TypeOccurrence>
    {
        public void Configure(EntityTypeBuilder<TypeOccurrence> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(x => x.Hash)
                   .IsUnique();

            builder.Property(x => x.Active)
                   .IsRequired()
                   .HasDefaultValue("True");

            builder.Property(x => x.DateCreated)
                   .HasColumnType("SmallDatetime");

            builder.Property(x => x.DateUpdated)
                   .HasColumnType("SmallDatetime");
            
            builder.Property(x => x.DateDeleted)
                   .HasColumnType("SmallDatetime");

            builder.HasMany(e => e.MarkerTypeOccurrences)
                   .WithOne(rs => rs.TypeOccurrence)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
