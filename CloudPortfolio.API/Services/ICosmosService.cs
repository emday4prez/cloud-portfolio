using CloudPortfolio.API.Models;

namespace CloudPortfolio.API.Services;

public interface ICosmosService
{
    // AZ-204: Querying items (SQL API)
    Task<IEnumerable<T>> GetItemsAsync<T>(string query) where T : BaseItem;

    // AZ-204: Point Read (Most efficient read)
    Task<T> GetItemAsync<T>(string id, string partitionKey) where T : BaseItem;

    // AZ-204: Creating items
    Task AddItemAsync<T>(T item) where T : BaseItem;

    // AZ-204: Updating items
    Task UpdateItemAsync<T>(string id, T item) where T : BaseItem;

    // AZ-204: Deleting items
    Task DeleteItemAsync<T>(string id, string partitionKey) where T : BaseItem;
}
