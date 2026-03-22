namespace CoordinateRegistration.Domain
{
    public class Marker
    {
        public int Id { get; set; }
        public Guid Hash { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }      
        public string Description { get; set; }
        public int PersonId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Person Person { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<MarkerTypeOccurrence> MarkerTypeOccurrences { get; set; }
        public IEnumerable<Comment> Comment { get; set; }

    }
}
