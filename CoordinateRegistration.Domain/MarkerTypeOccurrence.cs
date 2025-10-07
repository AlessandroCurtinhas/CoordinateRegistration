namespace CoordinateRegistration.Domain
{
    public class MarkerTypeOccurrence
    {
        public int Id { get; set; }
        public Guid Hash { get; set; }
        public int MarkerId { get; set; }
        public int TypeOccurrenceId { get; set; }
        public Marker Marker { get; set; }
        public TypeOccurrence? TypeOccurrence { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
