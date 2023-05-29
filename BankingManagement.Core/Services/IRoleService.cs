using BankingManagement.Core.DTOs.Response;
using BankingManagement.Core.DTOs.Role;

namespace BankingManagement.Core.Services;

public interface IRoleService
{
    Task<CustomResponseDto<IEnumerable<RoleDto>>> GetAllRolesAsync();
    Task<CustomResponseDto<RoleDto>> GetRoleByIdAsync(Guid id);
    Task<CustomResponseDto<RoleDto>> CreateRoleAsync(RoleCreateDto newRole);
    Task<CustomResponseDto<bool>> DeleteRoleAsync(Guid id);
}