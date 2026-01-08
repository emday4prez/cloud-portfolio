using Microsoft.Azure.Cosmos;
using CloudPortfolio.API.Models;

namespace CloudPortfolio.API.Services;

public class CosmosService : ICosmosService
{
    private readonly Container _container;

    public CosmosService(CosmosClient cosmosClient, string databaseName, string containerName)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task AddItemAsync<T>(T item) where T : BaseItem
    {
        // AZ-204: CreateItemAsync creates a new item in the container
        await _container.CreateItemAsync(item, new PartitionKey(item.Category));
    }

    public async Task DeleteItemAsync<T>(string id, string partitionKey) where T : BaseItem
    {
        // AZ-204: DeleteItemAsync requires PartitionKey
        await _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
    }

    public async Task<T> GetItemAsync<T>(string id, string partitionKey) where T : BaseItem
    {
        try
        {
            // AZ-204: ReadItemAsync is a "Point Read" - efficient, uses 1 RU
            ItemResponse<T> response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetItemsAsync<T>(string queryString) where T : BaseItem
    {
        var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
        var results = new List<T>();

        // AZ-204: Iterate through query results (feed iterator)
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task UpdateItemAsync<T>(string id, T item) where T : BaseItem
    {
        // AZ-204: UpsertItemAsync maps to "Replace" or "Insert" 
        // Note: ReplaceItemAsync is strictly update
        await _container.UpsertItemAsync(item, new PartitionKey(item.Category));
    }
}
