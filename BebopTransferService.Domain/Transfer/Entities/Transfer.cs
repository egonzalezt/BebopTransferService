namespace BebopTransferService.Domain.Transfer.Entities;

using Domain.User.Dtos;
using Domain.SharedKernel;
using Domain.File.Entities;

public class Transfer : Entity
{
    private Transfer(
        Guid id,
        Guid userId,
        string externalOperatorId,
        string email
    ) : base(id)
    {
        Email = email;
        UserId = userId;
        ExternalOperatorId = externalOperatorId;
    }

    private Transfer() { }

    public Guid UserId { get; private set; }
    public long UserIdentificationNumber { get; private set; }
    public string ExternalOperatorId { get; private set; }
    public string UserName { get; private set; }
    public string UserAddress { get; private set; }
    public string Email { get; private set; }
    public bool IsKeroAuthUserDisabled { get; private set; }
    public bool IsCoplandUserDisabled { get; private set; }
    public bool IsStandUserDisabled { get; private set; }
    public IEnumerable<File> Files { get; private set; }

    public void SetFiles( IEnumerable<File> files)
    {
        Files = files;
    }

    public void SetStandUserDisabled()
    {
        SetUpdated();
        IsStandUserDisabled = true;
    }

    public void SetCoplandDisabled()
    {
        SetUpdated();
        IsCoplandUserDisabled = true; 
    }

    public void SetKeroAuthDisabled()
    {
        SetUpdated();
        IsKeroAuthUserDisabled = true;
    }

    public static Transfer BuildFromUserTransferRequest(UserTransferRequestDto userTransferRequestDto)
    {
        return new(Guid.NewGuid(), userTransferRequestDto.UserId, userTransferRequestDto.OperatorId, userTransferRequestDto.UserEmail);
    }
}
