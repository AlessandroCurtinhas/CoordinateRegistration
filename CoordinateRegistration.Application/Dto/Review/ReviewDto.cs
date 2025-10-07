namespace CoordinateRegistration.Application.Dto.Review
{
    public class ReviewDto
    {
        public Guid Hash { get; set; }
        public Guid MarkerHash { get; set; }
        public string? UserName { get; set; }
        public bool? Positive { get; set; }
        public bool? Negative { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
