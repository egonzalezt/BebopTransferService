namespace BebopTransferService.Infrastructure.Cache.Options;

public class CacheOptions
{
    public string ConnectionString { get; set; }
    public string InstanceName { get; set; }
    public int ConnectionTimeout { get; set; } = 5;
}
