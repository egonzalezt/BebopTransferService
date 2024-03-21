namespace BebopTransferService.Application.UseCases;

using Application.Interfaces;
using System.Text.Json;
using Domain.Transfer.Repositories;
using Domain.Transfer;
using Domain.Transfer.Exceptions;
using Domain.User.Dtos;
using Domain.SharedKernel.Exceptions;
using Microsoft.Extensions.Logging;

public class StandUserResponseUseCase(ITransferCommandRepository transferCommandRepository, ITransferQueryRepository transferQueryRepository, ITransferToExternalProviderUseCase transferToExternalProvider, ILogger<StandUserResponseUseCase> logger) : IInternalServicesResponseUseCase
{
    public TransferOperations UseCase { get; } = TransferOperations.StandUserResponse;

    public async Task ExecuteAsync(string body, string userId)
    {
        logger.LogInformation("Start processing StandUsers request");
        var id = Guid.Parse(userId);
        var userDto = JsonSerializer.Deserialize<StandUserTransferResponseDto>(body) ?? throw new InvalidBodyException();
        var transfer = await transferCommandRepository.GetByUserIdAsync(id) ?? throw new TransferNotFoundException(userId);
        transfer.Update(userDto);
        transfer.SetStandUserDisabled();
        transferQueryRepository.Update(transfer);
        await transferToExternalProvider.TryTransferAsync(transfer);
        logger.LogInformation("StandUsers request complete");
        return;
    }
}
