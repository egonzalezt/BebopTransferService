﻿namespace BebopTransferService.Infrastructure.EntityFrameworkCore.Commands;

using DbContext;
using Domain.Transfer.Entities;
using Domain.Transfer.Repositories;
using Microsoft.EntityFrameworkCore;

internal class TransferCommandRepository(BebopDbContext context) : ITransferCommandRepository
{
    public async Task<bool> ExistsByUserIdAsync(Guid id)
    {
        return await context.Transfers.AnyAsync(u => u.UserId == id);
    }

    public async Task<Transfer?> GetByIdAsync(Guid id)
    {
        return await context.Transfers
            .Include(f => f.Files)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Transfer?> GetByUserIdAsync(Guid id)
    {
        return await context.Transfers
            .Include(f => f.Files)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }
}
