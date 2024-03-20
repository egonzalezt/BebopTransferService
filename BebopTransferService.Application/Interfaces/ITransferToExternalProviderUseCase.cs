namespace BebopTransferService.Application.Interfaces;

using Domain.Transfer.Entities;

public interface ITransferToExternalProviderUseCase
{
    Task TryTransferAsync(Transfer transfer);
}
