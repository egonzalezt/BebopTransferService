namespace BebopTransferService.Infrastructure.Configuration;

using Domain.Transfer;
using MessageBroker.Publisher.Publishers;
using EntityFrameworkCore;
using MessageBroker.Publisher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.GovCarpeta;
using Infrastructure.GovCarpeta;
using Infrastructure.Cache.Options;
using Infrastructure.Cache;
using Polly;
using Infrastructure.ExternalOperatorTransfer;
using Infrastructure.UserNotifier;
using StackExchange.Redis;

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
        services.AddSingleton<IAsyncPolicy<HttpResponseMessage>>(provider => GetRetryPolicy());
        services.AddHttpClient("GovCarpeta", client =>
        {
            client.BaseAddress = new Uri(configuration["GovCarpeta:BaseUrl"]);
        });
        services.AddHttpClient("ExternalOperators", client =>
        {
        }).AddPolicyHandler((provider, request) =>
        {
            return provider.GetRequiredService<IAsyncPolicy<HttpResponseMessage>>();
        });

        var cacheOptions = new CacheOptions();
        configuration.Bind("CacheOptions", cacheOptions);
        var redisOptions = new ConfigurationOptions
        {
            ConnectTimeout = cacheOptions.ConnectionTimeout
        };
        services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = redisOptions;
            options.Configuration = cacheOptions.ConnectionString;
            options.InstanceName = cacheOptions.InstanceName;
        });
        services.AddSingleton<ICacheStore, CacheStore>();
        services.AddScoped<IExternalOperatorNotifier, ExternalOperatorNotifier>();
        services.AddSingleton<IUserTransferCompleteNotification, UserTransferCompleteNotification>();
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return Policy.Handle<HttpRequestException>()
                     .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                     .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
