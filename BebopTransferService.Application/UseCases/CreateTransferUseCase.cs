﻿namespace BebopTransferService.Application.UseCases;

using Domain.Transfer.Repositories;
using Domain.Transfer.Entities;
using Domain.User.Dtos;
using Domain.Transfer;
using Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

internal class CreateTransferUseCase(
        ITransferCommandRepository transferCommandRepository, 
        ITransferQueryRepository transferQueryRepository,
        ILogger<CreateTransferUseCase> logger,
        ITransferNotifier transferNotifier
    ) : ICreateTransferUseCase
{
    public async Task<Transfer> ExecuteAsync(UserTransferRequestDto transferRequestDto)
    {
        logger.LogInformation("Transfer for the user {userId} begins", transferRequestDto.UserId);
        var transferExists = await transferCommandRepository.ExistsByUserIdAsync(transferRequestDto.UserId);
        var transfer = Transfer.BuildFromUserTransferRequest(transferRequestDto);
        if (!transferExists)
        {
            await transferQueryRepository.CreateAsync(transfer);
        }
        transferNotifier.NotifyTransferToInternalServices(transferRequestDto, TransferOperations.TransferUser.ToString());
        logger.LogInformation("Agile Developers services are notified to process the transfer");
        return transfer;
    }
}
