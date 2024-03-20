namespace BebopTransferService.Infrastructure.MessageBroker.Options;

public class PublisherConfiguration
{
    public string UserTransferRequestBroadcastQueue { get; set; }
    public string UserTransferNotificationQueue { get; set; }
}
