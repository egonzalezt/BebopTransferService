namespace BebopTransferService.Domain.Transfer.Dtos;

using Domain.File.Dtos;
using Transfer.Entities;
using System.Text.Json.Serialization;
using BebopTransferService.Domain.Transfer.Exceptions;

public class TransferPackageDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("address")]
    public string Address { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("callbackUrl")]
    public string CallbackUrl { get; set; }
    [JsonPropertyName("files")]
    public FileTransferDto[] Files { get; set; }

    public static TransferPackageDto BuildFromTransfer(Transfer transfer, string callbackUrl)
    {
        var files = transfer.Files.Select(FileTransferDto.BuildFromFile).ToArray();
        if(transfer.UserIdentificationNumber is null) 
        {
            throw new IdentificationNumberNotFound();
        }
        return new()
        {
            Id = transfer.UserIdentificationNumber.Value,
            Address = transfer.UserAddress,
            Email = transfer.Email,
            Files = files,
            Name = transfer.UserName,
            CallbackUrl = callbackUrl
        };
    }
}
