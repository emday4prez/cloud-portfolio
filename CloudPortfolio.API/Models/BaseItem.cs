using Newtonsoft.Json;

namespace CloudPortfolio.API.Models;

/// <summary>
/// Abstract base class for all items stored in Cosmos DB.
/// AZ-204: Every item in a container requires a unique 'id'.
/// </summary>
public abstract class BaseItem
{
    [JsonProperty("id")] // Maps the C# property to the required lowercase 'id' in Cosmos DB JSON
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The Partition Key for the item. See implementation_plan.md for strategy.
    /// AZ-204: Efficient partitioning is key to scaling.
    /// </summary>
    [JsonProperty("category")] 
    public abstract string Category { get; }
}
