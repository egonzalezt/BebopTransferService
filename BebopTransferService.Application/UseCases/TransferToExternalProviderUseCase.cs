namespace BebopTransferService.Application.UseCases;

using BebopTransferService.Domain.GovCarpeta;
using BebopTransferService.Domain.SharedKernel.Exceptions;
using BebopTransferService.Domain.Transfer.Dtos;
using Domain.Transfer.Entities;
using Interfaces;
using System.Threading.Tasks;

internal class TransferToExternalProviderUseCase(IGetOperatorUseCase getOperatorUseCase) : ITransferToExternalProviderUseCase
{
    public async Task TryTransferAsync(Transfer transfer)
    {
        if (!transfer.IsReadyToBeTransferred())
        {
            return;
        }

        var externalOperator = await getOperatorUseCase.GetOperatorAsync(transfer.ExternalOperatorId) ?? throw new OperatorNotFoundException();
        var transferPackage = TransferPackageDto.BuildFromTransfer(transfer, "");
        // TODO make a post reques to the URL externalOperator.TransferAPIURL

        return;
    }
}
