namespace CoordinateRegistration.Domain
{
    public class TypeOccurrence
    {
        public int Id { get; set; }
        public Guid Hash { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public int? PersonId { get; set; }
        public int? PersonUpdateId { get; set; }
        public int? PersonDeleteId { get; set; }
        public IEnumerable<MarkerTypeOccurrence> MarkerTypeOccurrences { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public Person? PersonCreate { get; set; }
        public Person? PersonUpdate { get; set; }
        public Person? PersonDelete { get; set; }


    }
}
