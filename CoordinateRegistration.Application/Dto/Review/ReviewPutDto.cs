namespace CoordinateRegistration.Application.Dto.Review
{
    public class ReviewPutDto
    {
        public Guid Hash { get; set; }
        public bool? Positive { get; set; }
        public bool? Negative { get; set; }
    }
}
