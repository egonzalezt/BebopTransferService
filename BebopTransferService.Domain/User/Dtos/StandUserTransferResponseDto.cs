namespace BebopTransferService.Domain.User.Dtos;

public class StandUserTransferResponseDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Direction { get; set; }
    public long IdentificationNumber { get; set; }
    public Guid Id { get; set; }
}
