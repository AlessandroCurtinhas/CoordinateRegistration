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
        public DbSet<Person> Person { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<ProfileUsr> ProfileUsr { get; set; }
        public DbSet<PersonProfile> PersonProfile { get; set; }
        public DbSet<PersonCity> PersonCity { get; set; }
        

        public CoordinateRegistrationDbContext(DbContextOptions<CoordinateRegistrationDbContext> options)
                : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.ApplyConfiguration(new TypeOccurenceMap());
            modelBuilder.ApplyConfiguration(new MarkerMap());
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new ReviewMap());
            modelBuilder.ApplyConfiguration(new ProfileUsrMap());
            modelBuilder.ApplyConfiguration(new PersonCityMap());

            modelBuilder.Entity<ProfileUsr>().HasData(
            new ProfileUsr { Id = 1, Hash = Guid.NewGuid(), Name = "Admin" },
            new ProfileUsr { Id = 2, Hash = Guid.NewGuid(), Name = "User" }
            );

            modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, Name = "Admin", Email = "admin@admin.com.br", Hash = Guid.NewGuid(), DateCreated = DateTime.Parse("01/01/2025"), Password = "4de93544234adffbb681ed60ffcfb941" }
            );

            modelBuilder.Entity<PersonProfile>().HasData(
            new PersonProfile { Id = 1, Hash = Guid.NewGuid(), ProfileId = 1, PersonId = 1 }
            );
            

        }

    }
}
