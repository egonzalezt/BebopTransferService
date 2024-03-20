namespace BebopTransferService.Domain.Transfer;

using Dtos;

public interface IExternalOperatorNotifier
{
    Task NotifyAsync(TransferPackageDto packageDto, string externalOperatorUrl);
}
