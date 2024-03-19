namespace BebopTransferService.Infrastructure.EntityFrameworkCore.Commands;

using BebopTransferService.Infrastructure.EntityFrameworkCore.DbContext;
using Domain.Transfer.Entities;
using Domain.Transfer.Repositories;
using Microsoft.EntityFrameworkCore;

internal class TransferCommandRepository(BebopDbContext context) : ITransferCommandRepository
{
    public async Task<bool> ExistsByUserIdAsync(Guid id)
    {
        return await context.Transfers.AnyAsync(u => u.Id == id);
    }

    public async Task<Transfer?> GetById(Guid id)
    {
        return await context.Transfers.FirstOrDefaultAsync(u => u.Id == id);
    }
}
