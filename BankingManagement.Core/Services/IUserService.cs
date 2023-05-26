using BankingManagement.Core.DTOs.User;

namespace BankingManagement.Core.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(Guid id);
    Task<UserDto> CreateUserAsync(UserCreateDto newUser);
    Task<UserDto> UpdateUserAsync(Guid id, UserUpdateDto updatedUser);
    Task<bool> DeleteUserAsync(Guid id);
}