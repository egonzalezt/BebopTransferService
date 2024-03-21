namespace BebopTransferService.Domain.Transfer.Repositories;

using File.Entities;
using Entities;
public interface ITransferQueryRepository
{
    Task CreateAsync(Transfer transfer);
    Task UpdateAsync(Transfer transfer);
    Task AddFilesAsync(IEnumerable<File> files);
}
