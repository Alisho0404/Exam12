using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enteties
{
    public class Role:BaseEntety
    {
        public required string Name { get; set; }
        public List<UserRole>? UserRole { get; set; }
    }
}
