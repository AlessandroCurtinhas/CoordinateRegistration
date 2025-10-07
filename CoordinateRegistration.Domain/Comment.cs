namespace CoordinateRegistration.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public Guid Hash { get; set; }
        public int MarkerId { get; set; }
        public int? UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Marker Marker { get; set; }
        public User User { get; set; }

    }
}
