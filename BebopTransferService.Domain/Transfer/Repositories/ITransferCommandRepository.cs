
namespace BebopTransferService.Domain.Transfer.Repositories;
using Entities;

public interface ITransferCommandRepository
{
    Task<bool> ExistsByUserIdAsync(Guid id);
    Task<Transfer?> GetByIdAsync(Guid id);
}
