namespace CoordinateRegistration.Application.Dto.Comment
{
    public class CommentPutDto
    {
        public Guid Hash { get; set; }
        public Guid MarkerHash { get; set; }
        public string Text { get; set; }
    }
}
