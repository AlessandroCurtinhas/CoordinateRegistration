namespace CoordinateRegistration.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public Guid Hash { get; set; }
        public int? UserId { get; set; }
        public int MarkerId { get; set; }
        public bool Positive { get; set; }
        public bool Negative { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public User? User { get; set; }
        public Marker Marker { get; set; }
    }
}
