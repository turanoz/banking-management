﻿namespace BankingManagement.Core.DTOs.User;

public class UserCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; } // Plain password for creation
}