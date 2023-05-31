using AutoMapper;
using BankingManagement.Core.DTOs.Account;
using BankingManagement.Core.DTOs.AuditLog;
using BankingManagement.Core.DTOs.Role;
using BankingManagement.Core.DTOs.Transaction;
using BankingManagement.Core.DTOs.User;
using BankingManagement.Core.Models;

namespace BankingManagement.Service.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {

        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserCreateDto, User>().ReverseMap();

        CreateMap<UserUpdateDto, User>().ReverseMap();
        
        CreateMap<Account, AccountDto>();

        CreateMap<AccountCreateDto, Account>();

        CreateMap<AccountUpdateDto, Account>();
        
        CreateMap<Transaction, TransactionDto>();
        CreateMap<TransactionCreateDto, Transaction>();

        CreateMap<AuditLog, AuditLogDto>();
        CreateMap<AuditLogCreateDto, AuditLog>();
        
        CreateMap<Role, RoleDto>();
        CreateMap<RoleCreateDto, Role>();
    }
}
