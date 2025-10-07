namespace CoordinateRegistration.Domain
{
    public class UserProfile
    {
        public int Id { get; set; }
        public Guid Hash { get; set; }
        public int UserId { get; set; }
        public int ProfileId { get; set; }
        public ProfileUsr Profile { get; set; }
    }
}
