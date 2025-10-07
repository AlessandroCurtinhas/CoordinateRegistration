using CoordinateRegistration.Domain;
using CoordinateRegistration.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
   
namespace CoordinateRegistration.Persistence.Context
{
    public class CoordinateRegistrationDbContext : DbContext
    {
        public DbSet<Marker> Marker { get; set; }
        public DbSet<TypeOccurrence> TypeOccurrence { get; set; }
        public DbSet<MarkerTypeOccurrence> MarkerTypeOccurrence { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<ProfileUsr> ProfileUsr { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        

        public CoordinateRegistrationDbContext(DbContextOptions<CoordinateRegistrationDbContext> options)
                : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.ApplyConfiguration(new TypeOccurenceMap());
            modelBuilder.ApplyConfiguration(new MarkerMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new  ReviewMap());
            modelBuilder.ApplyConfiguration(new ProfileUsrMap());

            modelBuilder.Entity<ProfileUsr>().HasData(
            new ProfileUsr { Id = 1, Hash = Guid.NewGuid(), Name = "Admin" },
            new ProfileUsr { Id = 2, Hash = Guid.NewGuid(), Name = "User" }
            );

            modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Admin", Email = "admin@admin.com.br", Hash = Guid.NewGuid(), DateCreated = DateTime.Parse("01/01/2025"), Password = "0f2797f2182804d0cc7f0b85d254c146" }
            );

            modelBuilder.Entity<UserProfile>().HasData(
            new UserProfile { Id = 1, Hash = Guid.NewGuid(), ProfileId = 1, UserId = 1 }
            );
            

        }

    }
}
