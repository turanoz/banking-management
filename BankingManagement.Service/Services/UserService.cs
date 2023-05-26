using AutoMapper;
using BankingManagement.Core.DTOs.User;
using BankingManagement.Core.Models;
using BankingManagement.Core.Services;
using BankingManagement.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace BankingManagement.Service.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; // Use AutoMapper for mapping between entities and DTOs

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAll().ToListAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateUserAsync(UserCreateDto newUser)
    {
        var userEntity = _mapper.Map<User>(newUser);
        userEntity.RoleId=  Guid.Parse("4ab7aa96-0179-4cc1-b94a-ae19718e8e0b");
        
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password, salt);

        userEntity.PasswordHash = passwordHash;
        userEntity.Salt = salt;
        
        
        await _unitOfWork.UserRepository.CreateAsync(userEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<UserDto> UpdateUserAsync(Guid id, UserUpdateDto updatedUser)
    {
        var userEntity = await _unitOfWork.UserRepository.GetByIdAsync(id);
        // Apply the updates
        _mapper.Map(updatedUser, userEntity);
        _unitOfWork.UserRepository.Update(userEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var userEntity = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (userEntity != null)
        {
            _unitOfWork.UserRepository.Delete(userEntity);
            await _unitOfWork.CommitAsync();
            return true;
        }
        return false;
    }
}
