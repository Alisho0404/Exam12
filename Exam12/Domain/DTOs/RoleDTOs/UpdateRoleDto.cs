using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.RoleDTOs
{
    public class UpdateRoleDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
