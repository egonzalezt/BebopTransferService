﻿namespace BebopTransferService.Infrastructure.EntityFrameworkCore.Queries;

using DbContext;
using Domain.Transfer.Entities;
using Domain.Transfer.Repositories;
using System.Threading.Tasks;

public class TransferQueryRepository(BebopDbContext context) : ITransferQueryRepository
{

    public async Task CreateAsync(Transfer transfer)
    {
        await context.Transfers.AddAsync(transfer);
        await context.SaveChangesAsync();
    }

    public void Update(Transfer transfer)
    {
        context.Transfers.Update(transfer);
    }
}
