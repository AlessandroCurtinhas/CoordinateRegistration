using CoordinateRegistration.Domain;
using Microsoft.EntityFrameworkCore;

namespace CoordinateRegistration.Persistence.Mappings
{
    public class PersonCityMap : IEntityTypeConfiguration<PersonCity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PersonCity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.State)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.UF)
                   .IsRequired()
                   .HasMaxLength(2);

        }
    }
}
