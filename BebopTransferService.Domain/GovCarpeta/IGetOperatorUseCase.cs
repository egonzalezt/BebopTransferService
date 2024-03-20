namespace BebopTransferService.Domain.GovCarpeta; 
using Dtos;

public interface IGetOperatorUseCase
{
    Task<OperatorDto?> GetOperatorAsync(string operatorId);
    Task<OperatorDto[]?> GetOperatorsAsync();
}
