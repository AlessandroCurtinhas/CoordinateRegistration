using CoordinateRegistration.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoordinateRegistration.Persistence.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Hash)
                   .IsUnique();

            builder.Property(x => x.Text)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.DateCreated)
                   .HasColumnType("SmallDatetime");

            builder.Property(x => x.DateUpdated)
                   .HasColumnType("SmallDatetime");
        }
    }
}
