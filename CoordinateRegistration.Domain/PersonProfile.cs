namespace CoordinateRegistration.Domain
{
    public class PersonProfile
    {
        public int Id { get; set; }
        public Guid Hash { get; set; }
        public int PersonId { get; set; }
        public int ProfileId { get; set; }
        public ProfileUsr Profile { get; set; }
    }
}
