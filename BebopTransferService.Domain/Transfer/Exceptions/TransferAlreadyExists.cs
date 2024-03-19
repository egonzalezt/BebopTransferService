using BebopTransferService.Domain.SharedKernel.Exceptions;

namespace BebopTransferService.Domain.Transfer.Exceptions;

public class TransferAlreadyExists : BusinessException
{
    public TransferAlreadyExists() : base()
    {
    }

    public TransferAlreadyExists(string message) : base($"Transfer {message} already exists")
    {
    }

    public TransferAlreadyExists(string message, Exception innerException) : base(message, innerException)
    {
    }
}

