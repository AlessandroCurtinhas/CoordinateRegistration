namespace CoordinateRegistration.Application.Dto.TypeOccurrence
{
    public class TypeOccurrenceDtoPerson
    {
        public Guid Hash { get; set; }
        public string Name { get; set; }
        public string? PersonNameCreated { get; set; }
        public string? PersonNameUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
