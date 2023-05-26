using AutoMapper;
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

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _unitOfWork.RoleRepository.GetAll().ToListAsync();
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }

    public async Task<RoleDto> GetRoleByIdAsync(Guid id)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
        return _mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto> CreateRoleAsync(RoleCreateDto newRole)
    {
        var roleEntity = _mapper.Map<Role>(newRole);
        await _unitOfWork.RoleRepository.CreateAsync(roleEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<RoleDto>(roleEntity);
    }

    public async Task<bool> DeleteRoleAsync(Guid id)
    {
        var roleEntity = await _unitOfWork.RoleRepository.GetByIdAsync(id);
        if (roleEntity != null)
        {
            _unitOfWork.RoleRepository.Delete(roleEntity);
            await _unitOfWork.CommitAsync();
            return true;
        }
        return false;
    }
}
