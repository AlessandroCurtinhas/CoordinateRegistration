namespace CoordinateRegistration.Application.Dto.TypeOccurrence
{
    public class TypeOccurrenceDto
    {
        public Guid Hash { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string? PersonNameCreated { get; set; }
        public string? PersonNameUpdated { get; set; }
        public string? PersonNameDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}