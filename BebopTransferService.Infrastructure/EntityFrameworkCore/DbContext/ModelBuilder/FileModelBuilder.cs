namespace BebopTransferService.Infrastructure.EntityFrameworkCore.DbContext.ModelBuilder;

using Domain.File.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal static class FileModelBuilder
{
    public static void Configure(this EntityTypeBuilder<File> builder)
    {
        builder.Property(p => p.UrlDocument)
            .HasMaxLength(1024)
            .IsRequired();
        builder.Property(p => p.DocumentTitle)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(p => p.Id).IsRequired();
        builder.HasIndex(p => p.Id).IsUnique();
    }
}
