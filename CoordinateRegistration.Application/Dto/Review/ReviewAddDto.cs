namespace CoordinateRegistration.Application.Dto.Review
{
    public class ReviewAddDto
    {
        public Guid MarkerHash { get; set; }
        public bool? Positive { get; set; }
        public bool? Negative { get; set; }
        
    }
}
