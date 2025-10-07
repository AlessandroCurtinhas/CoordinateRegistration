namespace CoordinateRegistration.Application.Dto.TypeOccurrence
{
    public class TypeOccurrenceDtoUser
    {
        public Guid Hash { get; set; }
        public string Name { get; set; }
        public string? UserNameCreated { get; set; }
        public string? UserNameUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
