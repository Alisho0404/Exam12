using Domain.DTOs.RoleDTOs;
using Domain.Filters;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.RoleService
{
    public interface IRoleService
    {
        Task<PagedResponse<List<GetRoleDto>>> GetRolesAsync(PaginationFilter filter);
        Task<Response<GetRoleDto>> GetRoleByIdAsync(int roleId);
        Task<Response<string>> CreateRoleAsync(CreateRoleDto createRole);
        Task<Response<string>> UpdateRoleAsync(UpdateRoleDto updateRole);
        Task<Response<bool>> DeleteRoleAsync(int roleId);
    }
}
