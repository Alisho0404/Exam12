using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enteties
{
    public class RoleClaim:BaseEntety
    {
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public string ClaimType { get; set; } = null!;
        public string ClaimValue { get; set; } = null!;
    }
}
