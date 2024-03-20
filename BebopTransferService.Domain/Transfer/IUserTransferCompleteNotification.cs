namespace BebopTransferService.Domain.Transfer;

using Domain.Transfer.Dtos;

public interface IUserTransferCompleteNotification
{
    void Notify(UserTransferCompleteDto userTransferCompleteDto, Guid userId);
}
