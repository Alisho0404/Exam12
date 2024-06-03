using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enteties
{
    public class Meeting:BaseEntety
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
