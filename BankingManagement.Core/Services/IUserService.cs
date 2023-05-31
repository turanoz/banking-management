using BankingManagement.Core.DTOs.Response;
using BankingManagement.Core.DTOs.User;

namespace BankingManagement.Core.Services;

public interface IUserService
{
    Task<CustomResponseDto<IEnumerable<UserDto>>> GetAllUsersAsync();
    Task<CustomResponseDto<UserDto>> GetUserByIdAsync(Guid id);
    Task<CustomResponseDto<UserDto>> CreateUserAsync(UserCreateDto newUser);
    Task<CustomResponseDto<UserUpdateDto>> UpdateUserAsync(Guid id, UserUpdateDto updatedUser);
    Task<CustomResponseDto<bool>> DeleteUserAsync(Guid id);
}