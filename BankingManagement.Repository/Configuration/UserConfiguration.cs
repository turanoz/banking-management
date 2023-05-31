using BankingManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingManagement.Repository.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(u => u.Address)
            .IsRequired(false)
            .HasMaxLength(255);
       
        builder
            .HasMany(u => u.Accounts)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.AuditLogs)
            .WithOne(al => al.User)
            .HasForeignKey(al => al.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}