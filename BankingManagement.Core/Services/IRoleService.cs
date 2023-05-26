using BankingManagement.Core.DTOs.Role;

namespace BankingManagement.Core.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    Task<RoleDto> GetRoleByIdAsync(Guid id);
    Task<RoleDto> CreateRoleAsync(RoleCreateDto newRole);
    Task<bool> DeleteRoleAsync(Guid id);
    // Note: Updating a role name might have wide-ranging effects, consider this carefully
}
