using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.NotificationDTOs
{
    public class UpdateNotificationDto
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public int UserId { get; set; }
        public string? Message { get; set; }
        public DateTimeOffset SentDateTime { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
