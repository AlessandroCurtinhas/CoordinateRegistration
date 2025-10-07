namespace CoordinateRegistration.Domain
{
    public class User
    {
        public int Id { get; set; }
        public Guid Hash { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? PasswordDateRequest { get; set; }
        public Guid? RecoveryHash { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public IEnumerable<UserProfile> Profile { get; set; }
    }
}
