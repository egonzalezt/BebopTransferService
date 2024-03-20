namespace BebopTransferService.Application.UseCases;

using Application.Interfaces;
using System.Text.Json;
using Domain.Transfer.Exceptions;
using Domain.Transfer.Repositories;
using Domain.Transfer;
using Domain.File.Dtos;
using Domain.SharedKernel.Exceptions;
using Domain.File.Entities;

public class CoplandResponseUseCase(ITransferCommandRepository transferCommandRepository, ITransferQueryRepository transferQueryRepository, ITransferToExternalProviderUseCase transferToExternalProvider) : IInternalServicesResponseUseCase
{
    public TransferOperations UseCase { get; } = TransferOperations.CoplandResponse;

    public async Task ExecuteAsync(string body, string userId)
    {
        var id = Guid.Parse(userId);
        var filesDto = JsonSerializer.Deserialize<IEnumerable<FileDto>>(body) ?? throw new InvalidBodyException();
        var transfer = await transferCommandRepository.GetByIdAsync(id) ?? throw new TransferNotFoundException(userId);
        var files = filesDto.Select(File.BuildFromDto).ToList();
        transfer.SetFiles(files);
        transfer.SetCoplandDisabled();
        transferQueryRepository.Update(transfer);
        await transferToExternalProvider.TryTransferAsync(transfer);
        return;
    }
}
