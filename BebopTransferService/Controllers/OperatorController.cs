namespace BebopTransferService.Controllers;

using Domain.GovCarpeta;
using Domain.GovCarpeta.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class OperatorController(IGetOperatorUseCase getOperatorUseCase) : ControllerBase
{
    [HttpGet(Name = "Operators")]
    public async Task<ActionResult<OperatorDto[]?>> GetAsync()
    {
        var operators = await getOperatorUseCase.GetOperatorsAsync();
        if(operators is null)
        {
            return Ok();
        }
        var operatorsWithTransferUrl = operators.Where(op => op.TransferAPIURL is not null || !string.IsNullOrEmpty(op.TransferAPIURL));
        return Ok(operatorsWithTransferUrl);
    }
}
