using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateRegistration.Application.Dto.Authentication
{
    public class UserRecoveryPasswordResponse
    {
        public string? Email { get; set; }
        public Guid? RecoveryHash { get; set; }
        public DateTime? Date { get; set; }
    }
}
