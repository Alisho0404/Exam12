using Domain.DTOs.RoleDTOs;
using Domain.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.UseRoleDTOs
{
    public class GetUserRoleDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public DateTimeOffset UpdateAt { get; set; }
        public GetRoleDto? Role { get; set; }
        public GetUserDto? User { get; set; }
    }
}
