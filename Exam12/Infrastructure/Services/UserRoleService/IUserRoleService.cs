using Domain.DTOs.UseRoleDTOs;
using Domain.Filters;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.UserRoleService
{
    public interface IUserRoleService
    {
        Task<PagedResponse<List<GetUserRoleDto>>> GetUserRolesAsync(PaginationFilter filter);
        Task<Response<GetUserRoleDto>> GetUserRoleByIdAsync(UserRoleDto userRoleDto);
        Task<Response<string>> CreateUserRoleAsync(CreateUserRoleDto createUserRole);
        Task<Response<bool>> DeleteUserRoleAsync(UserRoleDto userRole);
    }
}
