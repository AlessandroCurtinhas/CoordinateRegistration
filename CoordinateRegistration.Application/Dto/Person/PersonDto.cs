namespace CoordinateRegistration.Application.Dto.Person
{
    public class PersonDto
    {
        public Guid Hash { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
