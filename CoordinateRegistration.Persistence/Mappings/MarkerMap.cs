using CoordinateRegistration.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoordinateRegistration.Persistence.Mappings
{
    public class MarkerMap : IEntityTypeConfiguration<Marker>
    {
        public void Configure(EntityTypeBuilder<Marker> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Description)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasIndex(x => x.Hash)
                   .IsUnique();

            builder.Property(x => x.DateCreated)
                   .HasColumnType("SmallDatetime");

            builder.Property(x => x.DateUpdated)
                   .HasColumnType("SmallDatetime");

            builder.HasMany(e => e.MarkerTypeOccurrences)
                   .WithOne(rs => rs.Marker)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
