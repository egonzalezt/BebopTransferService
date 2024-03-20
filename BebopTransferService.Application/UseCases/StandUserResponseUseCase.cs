namespace BebopTransferService.Application.UseCases;

using Application.Interfaces;
using System.Text.Json;
using Domain.Transfer.Repositories;
using Domain.Transfer;
using Domain.Transfer.Exceptions;
using Domain.User.Dtos;
using Domain.SharedKernel.Exceptions;

public class StandUserResponseUseCase(ITransferCommandRepository transferCommandRepository, ITransferQueryRepository transferQueryRepository, ITransferToExternalProviderUseCase transferToExternalProvider) : IInternalServicesResponseUseCase
{
    public TransferOperations UseCase { get; } = TransferOperations.StandUserResponse;

    public async Task ExecuteAsync(string body, string userId)
    {
        var id = Guid.Parse(userId);
        var userDto = JsonSerializer.Deserialize<StandUserTransferResponseDto>(body) ?? throw new InvalidBodyException();
        var transfer = await transferCommandRepository.GetByIdAsync(id) ?? throw new TransferNotFoundException(userId);
        transfer.Update(userDto);
        transfer.SetStandUserDisabled();
        transferQueryRepository.Update(transfer);
        await transferToExternalProvider.TryTransferAsync(transfer);
        return;
    }
}
