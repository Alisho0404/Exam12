using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enteties
{
    public class User : BaseEntety
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string? Password { get; set; }
        public string? Code { get; set; }
        public DateTimeOffset CodeTime { get; set; }
        public List<UserRole>? UserRole { get; set; }
        public List<Notification>? Notifications { get; set; }
    }
}
