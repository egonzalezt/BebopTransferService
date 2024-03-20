using BebopTransferService.Domain.Transfer;

namespace BebopTransferService.Application.Interfaces;

public interface IInternalServicesResponseUseCase
{
    TransferOperations UseCase { get; }
    public Task ExecuteAsync(string body, string userId);
}
