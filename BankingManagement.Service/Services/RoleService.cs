using AutoMapper;
using BankingManagement.Core.DTOs.Response;
using BankingManagement.Core.DTOs.Role;
using BankingManagement.Core.Models;
using BankingManagement.Core.Services;
using BankingManagement.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace BankingManagement.Service.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; // Use AutoMapper for mapping between entities and Dtos

    public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<IEnumerable<RoleDto>>> GetAllRolesAsync()
    {
        var roles = await _unitOfWork.RoleRepository.GetAll().ToListAsync();
        return CustomResponseDto<IEnumerable<RoleDto>>.Success(_mapper.Map<IEnumerable<RoleDto>>(roles),
            "Roles found.");
    }

    public async Task<CustomResponseDto<RoleDto>> GetRoleByIdAsync(Guid id)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
        return role is null
            ? CustomResponseDto<RoleDto>.Error("Role not found.")
            : CustomResponseDto<RoleDto>.Success(_mapper.Map<RoleDto>(role), "Role found.");
    }

    public async Task<CustomResponseDto<RoleDto>> CreateRoleAsync(RoleCreateDto newRole)
    {
        var roleEntity = _mapper.Map<Role>(newRole);
        await _unitOfWork.RoleRepository.CreateAsync(roleEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<RoleDto>.Success(_mapper.Map<RoleDto>(roleEntity), "Role created.");
    }

    public async Task<CustomResponseDto<bool>> DeleteRoleAsync(Guid id)
    {
        var roleEntity = await _unitOfWork.RoleRepository.GetByIdAsync(id);
        if (roleEntity is null)
        {
            return CustomResponseDto<bool>.Error("Role not found.");
        }

        _unitOfWork.RoleRepository.Delete(roleEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<bool>.Success(true, "Role deleted.");
    }
}