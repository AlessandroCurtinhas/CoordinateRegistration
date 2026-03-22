namespace CoordinateRegistration.Application.Dto.Authentication
{
    public class PersonRecoveryPasswordDto
    {
        public Guid? RecoveryHash { get; set; }
        public string? Password { get; set; }
        public string? ConfirmedPassword { get; set; }
    }
}
