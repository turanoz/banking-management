using BankingManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingManagement.Repository.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(u=>u.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.TransactionType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.TransactionTime)
            .IsRequired();
        
        builder.HasOne(e => e.Account)
            .WithMany(e => e.Transactions)
            .HasForeignKey(e => e.AccountId);

        builder.HasOne(e => e.ReceiverAccount)
            .WithMany()
            .HasForeignKey(e => e.ReceiverAccountId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}
