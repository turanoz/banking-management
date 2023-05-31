using AutoMapper;
using BankingManagement.Core.DTOs.Response;
using BankingManagement.Core.DTOs.User;
using BankingManagement.Core.Models;
using BankingManagement.Core.Services;
using BankingManagement.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace BankingManagement.Service.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<IEnumerable<UserDto>>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAll().ToListAsync();
        return CustomResponseDto<IEnumerable<UserDto>>.Success(_mapper.Map<IEnumerable<UserDto>>(users),
            "Users found.");
    }

    public async Task<CustomResponseDto<UserDto>> GetUserByIdAsync(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        return user is null
            ? CustomResponseDto<UserDto>.Error("User not found.")
            : CustomResponseDto<UserDto>.Success(_mapper.Map<UserDto>(user), "User found.");
    }

    public async Task<CustomResponseDto<UserDto>> CreateUserAsync(UserCreateDto newUser)
    {
        var userEntity = _mapper.Map<User>(newUser);
        userEntity.RoleId = Guid.Parse("4ab7aa96-0179-4cc1-b94a-ae19718e8e0b");

        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password, salt);

        userEntity.PasswordHash = passwordHash;
        userEntity.Salt = salt;


        await _unitOfWork.UserRepository.CreateAsync(userEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<UserDto>.Success(_mapper.Map<UserDto>(userEntity), "User created.");
    }

    public async Task<CustomResponseDto<UserUpdateDto>> UpdateUserAsync(Guid id, UserUpdateDto updatedUser)
    {
        var userEntity = await _unitOfWork.UserRepository.GetByIdAsync(id);
        _mapper.Map(updatedUser, userEntity);
        _unitOfWork.UserRepository.Update(userEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<UserUpdateDto>.Success(_mapper.Map<UserUpdateDto>(userEntity), "User updated.");
    }

    public async Task<CustomResponseDto<bool>> DeleteUserAsync(Guid id)
    {
        var userEntity = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (userEntity is null)
        {
            return CustomResponseDto<bool>.Error("User not found.");
        }

        _unitOfWork.UserRepository.Delete(userEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<bool>.Success(true, "User deleted.");
    }
}