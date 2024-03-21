namespace BebopTransferService.Controllers;

using Domain.GovCarpeta;
using Domain.GovCarpeta.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class OperatorController(IGetOperatorUseCase getOperatorUseCase) : ControllerBase
{
    [HttpGet("List")]
    public async Task<ActionResult<OperatorDto[]?>> GetAsync()
    {
        var operators = await getOperatorUseCase.GetOperatorsAsync();
        if(operators is null)
        {
            return Ok();
        }
        var operatorsWithTransferUrl = operators.Where(op => op.TransferAPIURL is not null || !string.IsNullOrEmpty(op.TransferAPIURL)).Where(o => o.OperatorId != "65ed0ef4fe35fb0019dc3297");
        return Ok(operatorsWithTransferUrl);
    }
}
