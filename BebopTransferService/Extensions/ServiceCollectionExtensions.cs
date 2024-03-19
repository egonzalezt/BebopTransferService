namespace BebopTransferService.Extensions;

using Workers.Extensions;
using Infrastructure.Configuration;
using Application.Services;
using Frieren_Guard.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFrierenGuardServices(configuration);
        services.AddInfrastructure(configuration);
        services.AddWorkers(configuration);
        services.AddApplication();
    }
}
