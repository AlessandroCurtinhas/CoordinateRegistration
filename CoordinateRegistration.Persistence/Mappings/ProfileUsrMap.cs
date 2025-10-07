using CoordinateRegistration.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoordinateRegistration.Persistence.Mappings
{
    internal class ProfileUsrMap : IEntityTypeConfiguration<ProfileUsr>
    {
        public void Configure(EntityTypeBuilder<ProfileUsr> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasIndex(x => x.Hash)
                   .IsUnique();

        }
    }
}
