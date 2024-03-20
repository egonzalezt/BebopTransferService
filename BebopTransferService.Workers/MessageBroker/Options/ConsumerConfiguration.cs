namespace BebopTransferService.Workers.MessageBroker.Options;

public class ConsumerConfiguration
{
    public string TransferUserQueue { get; set; }
    public string TransterUserReplyQueue { get; set; }
}
