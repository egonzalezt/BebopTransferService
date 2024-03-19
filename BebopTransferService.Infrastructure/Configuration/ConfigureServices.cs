namespace BebopTransferService.Infrastructure.Configuration;

using Domain.Transfer;
using MessageBroker.Publisher.Publishers;
using EntityFrameworkCore;
using MessageBroker.Publisher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEntityFramework(configuration);
        services.AddRepositories();
        services.AddSingleton<IMessageSender, RabbitMQMessageSender>();
        services.AddSingleton<ITransferNotifier, TransferNotifierSender>();
    }
}
