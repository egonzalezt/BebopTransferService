namespace BebopTransferService.Infrastructure.EntityFrameworkCore.Queries;

using DbContext;
using Domain.Transfer.Entities;
using Domain.Transfer.Repositories;
using System.Threading.Tasks;

public class TransferQueryRepository(BebopDbContext context) : ITransferQueryRepository
{

    public async Task CreateAsync(Transfer transfer)
    {
        context.Transfers.Add(transfer);
        await context.SaveChangesAsync();
    }

    public void Update(Transfer transfer)
    {
        context.Update(transfer);
    }
}
