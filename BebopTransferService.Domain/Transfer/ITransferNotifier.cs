namespace BebopTransferService.Domain.Transfer;

using User.Dtos;

public interface ITransferNotifier
{
    void NotifyTransferToInternalServices(UserTransferRequestDto transferRequest, string messageType);
}
