namespace BebopTransferService.Application.Services;

using Interfaces;
using UseCases;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureApplication
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateTransferUseCase, CreateTransferUseCase>();
    }
}
