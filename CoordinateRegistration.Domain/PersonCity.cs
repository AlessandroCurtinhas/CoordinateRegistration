using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateRegistration.Domain
{
    public class PersonCity
    {
        public int Id { get; set; }
        public Guid Hash { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string UF { get; set; }
        public int PersonId { get; set; }

    }
}
