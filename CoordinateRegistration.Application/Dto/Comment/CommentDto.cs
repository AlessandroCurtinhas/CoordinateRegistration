namespace CoordinateRegistration.Application.Dto.Comment
{
    public class CommentDto
    {
        public Guid CommentHash { get; set; }
        public Guid MarkerHash { get; set; }
        public string? UserName { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        
    }
}
