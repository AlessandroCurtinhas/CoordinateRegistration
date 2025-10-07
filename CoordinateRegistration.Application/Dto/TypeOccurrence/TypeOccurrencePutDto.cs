namespace CoordinateRegistration.Application.Dto.TypeOccurrence
{
    public class TypeOccurrencePutDto
    {
        public Guid Hash { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}