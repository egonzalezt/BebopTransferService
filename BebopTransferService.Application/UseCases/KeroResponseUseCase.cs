namespace BebopTransferService.Application.UseCases;

using Application.Interfaces;
using Domain.Transfer.Repositories;
using Domain.Transfer;
using Domain.Transfer.Exceptions;

public class KeroResponseUseCase(ITransferCommandRepository transferCommandRepository, ITransferQueryRepository transferQueryRepository, ITransferToExternalProviderUseCase transferToExternalProvider) : IInternalServicesResponseUseCase
{
    public TransferOperations UseCase { get; } = TransferOperations.KeroResponse;

    public async Task ExecuteAsync(string body, string userId)
    {
        var id = Guid.Parse(userId);
        var transfer = await transferCommandRepository.GetByUserIdAsync(id) ?? throw new TransferNotFoundException(userId);
        transfer.SetKeroAuthDisabled();
        transferQueryRepository.Update(transfer);
        await transferToExternalProvider.TryTransferAsync(transfer);
        return;
    }
}
