namespace BebopTransferService.Infrastructure.ExternalOperatorTransfer;

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Polly;
using Exceptions;
using Domain.Transfer.Dtos;
using Domain.Transfer;

internal class ExternalOperatorNotifier(IHttpClientFactory httpClientFactory, IAsyncPolicy<HttpResponseMessage> retryPolicy) : IExternalOperatorNotifier
{
    private readonly HttpClient _externalOperatorsClient = httpClientFactory.CreateClient("ExternalOperators");

    public async Task NotifyAsync(TransferPackageDto packageDto, string externalOperatorUrl)
    {
        string jsonData = JsonSerializer.Serialize(packageDto);
        Exception? lastException = null;

        try
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _externalOperatorsClient.PostAsync(externalOperatorUrl, content);
            response.EnsureSuccessStatusCode();
            return;
        }
        catch (Exception ex)
        {
            lastException = ex;
            await retryPolicy.ExecuteAsync(() =>
            {
                throw lastException;
            });
        }
        throw new OperatorUnreachableException($"Operator unreachable: {externalOperatorUrl}", lastException);
    }
}
