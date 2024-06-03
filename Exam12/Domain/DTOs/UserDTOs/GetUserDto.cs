using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.UserDTOs
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
