using BankingManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingManagement.Repository.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.AccountId);

        builder.Property(a => a.AccountNumber)
            .IsRequired();

        builder.Property(a => a.AccountType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Balance)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.OpenedDate)
            .IsRequired();

        builder.HasOne(a => a.User)
            .WithMany(u => u.Accounts)
            .HasForeignKey(a => a.UserId);
    }
}
