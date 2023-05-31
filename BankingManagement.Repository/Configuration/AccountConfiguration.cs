using BankingManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingManagement.Repository.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.Number).IsRequired();

        builder.Property(a => a.Type).IsRequired().HasMaxLength(50);

        builder.Property(a => a.Balance).IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(a => a.OpenedDate)
            .IsRequired();
        
        builder.HasMany(e => e.Transactions)
            .WithOne(e => e.Account)
            .HasForeignKey(e => e.AccountId);
    }
}