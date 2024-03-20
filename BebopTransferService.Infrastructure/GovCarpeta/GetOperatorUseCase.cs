namespace BebopTransferService.Infrastructure.GovCarpeta;

using Domain.GovCarpeta.Dtos;
using Domain.GovCarpeta;
using System.Threading.Tasks;
using Infrastructure.Cache;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;

internal class GetOperatorUseCase(IOperatorsHttpClient operatorsHttpClient, ICacheStore cacheStore, ILogger<GetOperatorUseCase> logger) : IGetOperatorUseCase
{
    private const string Key = "Operators";

    public async Task<OperatorDto?> GetOperatorAsync(string operatorId)
    {
        var operators = await GetOperatorsFromGovCarpetaAsync();
        return operators?.SingleOrDefault(op => op.OperatorId == operatorId);
    }

    public async Task<OperatorDto[]?> GetOperatorsAsync()
    {
        var operators = await GetOperatorsFromGovCarpetaAsync();
        return operators;
    }

    private async Task<OperatorDto[]?> GetOperatorsFromGovCarpetaAsync()
    {
        var operators = await TryGetOperatorsFromCacheAsync();

        if (operators == null || operators.Length == 0)
        {
            operators = await FetchOperatorsAsync();
            if (operators != null && operators.Length > 0)
            {
                await SaveOperatorsToCacheAsync(operators);
            }
        }

        return operators ?? [];
    }

    private async Task<OperatorDto[]?> TryGetOperatorsFromCacheAsync()
    {
        try
        {
            var serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return await cacheStore.GetAsync<OperatorDto[]>(Key, serializerOptions);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Error retrieving operators from cache: {ex.Message}");
            return null;
        }
    }

    private async Task SaveOperatorsToCacheAsync(OperatorDto[] operators)
    {
        var serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(15) };

        try
        {
            await cacheStore.SaveAsync(Key, operators, cacheOptions, serializerOptions);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Error saving operators to cache: {ex.Message}");
        }
    }

    private async Task<OperatorDto[]?> FetchOperatorsAsync()
    {
        return await operatorsHttpClient.ExecuteAsync(); 
    }
}
