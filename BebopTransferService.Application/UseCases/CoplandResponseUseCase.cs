namespace BebopTransferService.Application.UseCases;

using Application.Interfaces;
using System.Text.Json;
using Domain.Transfer.Exceptions;
using Domain.Transfer.Repositories;
using Domain.Transfer;
using Domain.File.Dtos;
using Domain.SharedKernel.Exceptions;
using Domain.File.Entities;
using Microsoft.Extensions.Logging;

public class CoplandResponseUseCase(ITransferCommandRepository transferCommandRepository, ITransferQueryRepository transferQueryRepository, ITransferToExternalProviderUseCase transferToExternalProvider, ILogger<CoplandResponseUseCase> logger) : IInternalServicesResponseUseCase
{
    public TransferOperations UseCase { get; } = TransferOperations.CoplandResponse;

    public async Task ExecuteAsync(string body, string userId)
    {
        logger.LogInformation("Start processing Copland request");
        var id = Guid.Parse(userId);
        var filesDto = JsonSerializer.Deserialize<IEnumerable<FileDto>>(body) ?? throw new InvalidBodyException();
        var transfer = await transferCommandRepository.GetByUserIdAsync(id) ?? throw new TransferNotFoundException(userId);
        var files = filesDto.Select(f => File.BuildFromDto(f,transfer.Id)).ToList();
        transfer.SetCoplandDisabled();
        await transferQueryRepository.AddFilesAsync(files);
        await transferQueryRepository.UpdateAsync(transfer);
        await transferToExternalProvider.TryTransferAsync(transfer);
        logger.LogInformation("Copland request complete");
        return;
    }
}
