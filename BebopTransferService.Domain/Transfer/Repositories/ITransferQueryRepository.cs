namespace BebopTransferService.Domain.Transfer.Repositories;

using Entities;
public interface ITransferQueryRepository
{
    Task CreateAsync(Transfer transfer);
    void Update(Transfer transfer);
}
