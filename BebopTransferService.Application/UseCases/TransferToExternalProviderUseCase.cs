namespace BebopTransferService.Application.UseCases;

using Domain.Transfer;
using Domain.Transfer.Exceptions;
using Domain.GovCarpeta;
using Domain.SharedKernel.Exceptions;
using Domain.Transfer.Dtos;
using Domain.Transfer.Entities;
using Interfaces;
using System.Threading.Tasks;

internal class TransferToExternalProviderUseCase(IGetOperatorUseCase getOperatorUseCase, IExternalOperatorNotifier externalOperatorNotifier, IUserTransferCompleteNotification userTransferCompleteNotification) : ITransferToExternalProviderUseCase
{
    public async Task TryTransferAsync(Transfer transfer)
    {
        if (!transfer.IsReadyToBeTransferred())
        {
            return;
        }

        (var requestedOperator, var transferReplyUrl) = await getOperatorUseCase.GetOperatorAsync(transfer.ExternalOperatorId);
        if (requestedOperator is null)
        {
            throw new OperatorNotFoundException();
        }
        if(string.IsNullOrEmpty(transferReplyUrl))
        {
            throw new TransferReplyUrlNotFoundException();
        }
        var externalOperatorUrl = requestedOperator.TransferAPIURL;
        if (string.IsNullOrEmpty(externalOperatorUrl))
        {
            throw new ExternalOperatorTransferUrlNotFoundException();
        }
        var modifiedTransferReplyUrl = $"{transferReplyUrl}?userId={transfer.UserId}";
        var transferPackage = TransferPackageDto.BuildFromTransfer(transfer, modifiedTransferReplyUrl);
        await externalOperatorNotifier.NotifyAsync(transferPackage, externalOperatorUrl);
        userTransferCompleteNotification.Notify(new UserTransferCompleteDto { Email = transfer.Email, Name = transfer.UserName, TicketId = transfer.Id }, transfer.UserId);
        return;
    }
}
