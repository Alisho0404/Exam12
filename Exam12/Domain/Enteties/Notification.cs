using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enteties
{
    public class Notification:BaseEntety
    {
        public int MeetingId { get; set; }
        public int UserId { get; set; }
        public string? Message { get; set; }
        public DateTimeOffset SentDateTime { get; set; }
        public Meeting? Meeting { get; set; }
    }
}
