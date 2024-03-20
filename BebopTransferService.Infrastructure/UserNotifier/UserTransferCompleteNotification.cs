namespace BebopTransferService.Infrastructure.UserNotifier;

using Domain.Transfer.Dtos;
using Domain.Transfer;
using Microsoft.Extensions.Options;
using MessageBroker;
using MessageBroker.Options;
using MessageBroker.Publisher;
using MessageBroker.Publisher.Publishers;
using Microsoft.Extensions.Logging;

internal class UserTransferCompleteNotification(
    IOptions<PublisherConfiguration> options,
    IMessageSender messageSender,
    ILogger<TransferNotifierSender> logger
) : IUserTransferCompleteNotification
{
    private readonly PublisherConfiguration _publisherQueues = options.Value;
    public void Notify(UserTransferCompleteDto userTransferCompleteDto, Guid userId)
    {
        logger.LogInformation("Sending request to notification system for user transferred notification");
        var headers = new EventHeaders(TransferOperations.TransferUser.ToString(), userId);
        messageSender.SendMessage(userTransferCompleteDto, _publisherQueues.UserTransferNotificationQueue, headers.GetAttributesAsDictionary());
        logger.LogInformation("Message sent");
    }
}
