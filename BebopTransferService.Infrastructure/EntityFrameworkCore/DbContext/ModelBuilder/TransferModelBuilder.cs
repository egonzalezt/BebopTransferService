namespace BebopTransferService.Infrastructure.EntityFrameworkCore.DbContext.ModelBuilder;

using Domain.Transfer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal static class TransferModelBuilder
{
    public static void Configure(this EntityTypeBuilder<Transfer> builder)
    {
        builder.Property(p => p.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.UserIdentificationNumber)
          .HasMaxLength(10);

        builder.Property(p => p.ExternalOperatorId)
          .HasMaxLength(200)
          .IsRequired();

        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.HasIndex(p => p.Id).IsUnique();
        builder.HasIndex(p => p.UserId).IsUnique();
    }
}
