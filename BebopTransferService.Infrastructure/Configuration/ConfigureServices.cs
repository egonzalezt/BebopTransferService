namespace BebopTransferService.Infrastructure.Configuration;

using Domain.Transfer;
using MessageBroker.Publisher.Publishers;
using EntityFrameworkCore;
using MessageBroker.Publisher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.GovCarpeta;
using Infrastructure.GovCarpeta;
using BebopTransferService.Infrastructure.Cache.Options;
using BebopTransferService.Infrastructure.Cache;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEntityFramework(configuration);
        services.AddRepositories();
        services.AddSingleton<IMessageSender, RabbitMQMessageSender>();
        services.AddSingleton<ITransferNotifier, TransferNotifierSender>();
        services.AddSingleton<IGetOperatorUseCase, GetOperatorUseCase>();
        services.AddSingleton<IOperatorsHttpClient, OperatorsHttpClient>();
        services.AddHttpClient("GovCarpeta", client =>
        {
            client.BaseAddress = new Uri(configuration["GovCarpeta:BaseUrl"]);
        });
        var cacheOptions = new CacheOptions();
        configuration.Bind("CacheOptions", cacheOptions);
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = cacheOptions.ConnectionString;
            options.InstanceName = cacheOptions.InstanceName;
        });
        services.AddSingleton<ICacheStore, CacheStore>();
    }
}
