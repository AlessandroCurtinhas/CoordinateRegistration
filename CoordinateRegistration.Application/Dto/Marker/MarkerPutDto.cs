namespace CoordinateRegistration.Application.Dto.Marker
{
    public class MarkerPutDto
    {
        public Guid Hash { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public IEnumerable<Guid> TypeOccurrenceHash { get; set; }
        public string Description { get; set; }
    }
}
