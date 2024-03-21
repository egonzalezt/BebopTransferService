namespace BebopTransferService.Application.UseCases;

using Application.Interfaces;
using Domain.Transfer.Repositories;
using Domain.Transfer;
using Domain.Transfer.Exceptions;
using Microsoft.Extensions.Logging;

public class KeroResponseUseCase(ITransferCommandRepository transferCommandRepository, ITransferQueryRepository transferQueryRepository, ITransferToExternalProviderUseCase transferToExternalProvider, ILogger<KeroResponseUseCase> logger) : IInternalServicesResponseUseCase
{
    public TransferOperations UseCase { get; } = TransferOperations.KeroResponse;

    public async Task ExecuteAsync(string body, string userId)
    {
        logger.LogInformation("Start processing Kero request");
        var id = Guid.Parse(userId);
        var transfer = await transferCommandRepository.GetByUserIdAsync(id) ?? throw new TransferNotFoundException(userId);
        transfer.SetKeroAuthDisabled();
        await transferQueryRepository.UpdateAsync(transfer);
        await transferToExternalProvider.TryTransferAsync(transfer);
        logger.LogInformation("Kero request complete");
        return;
    }
}
