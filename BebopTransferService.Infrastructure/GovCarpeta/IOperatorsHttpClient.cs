using BebopTransferService.Domain.GovCarpeta.Dtos;

namespace BebopTransferService.Infrastructure.GovCarpeta;

public interface IOperatorsHttpClient
{
    Task<OperatorDto[]?> ExecuteAsync();
}
