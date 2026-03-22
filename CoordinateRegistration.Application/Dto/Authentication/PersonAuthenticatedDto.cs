namespace CoordinateRegistration.Application.Dto.Authentication
{
    public class PersonAuthenticatedDto
    {
        public Guid? Hash { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? AuthToken { get; set; }
    }
}
