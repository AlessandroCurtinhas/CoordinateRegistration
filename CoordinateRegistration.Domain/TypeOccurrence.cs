namespace CoordinateRegistration.Domain
{
    public class TypeOccurrence
    {
        public int Id { get; set; }
        public Guid Hash { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public int? UserUpdateId { get; set; }
        public int? UserDeleteId { get; set; }
        public IEnumerable<MarkerTypeOccurrence> MarkerTypeOccurrences { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public User? UserCreate { get; set; }
        public User? UserUpdate { get; set; }
        public User? UserDelete { get; set; }


    }
}
