namespace CoordinateRegistration.Application.JwtAuthentication
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationInHours { get; set; }
    }
}
