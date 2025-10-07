namespace CoordinateRegistration.Application.Dto.Marker
{
    public class MarkerDto
    {
        public Guid Hash { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }      
        public string Description { get; set; }
        public int PositiveTotal { get; set; }
        public double PositivePercentual { get; set; }
        public string? UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public IEnumerable<MarkerDtoTypeOccurrence> TypeOcurrences { get; set; }
    }
}
