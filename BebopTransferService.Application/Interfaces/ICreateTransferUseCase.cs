namespace BebopTransferService.Application.Interfaces;

using Domain.Transfer.Entities;
using Domain.User.Dtos;

public interface ICreateTransferUseCase
{
    Task<Transfer> ExecuteAsync(UserTransferRequestDto transferRequestDto);
}
