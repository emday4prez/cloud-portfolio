using Newtonsoft.Json;

namespace CloudPortfolio.API.Models;

public class PortfolioProject : BaseItem
{
    // AZ-204: Partition Key Implementation
    public override string Category => "Project";

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("imageUrl")]
    public string? ImageUrl { get; set; }

    [JsonProperty("tags")]
    public List<string> Tags { get; set; } = new();

    [JsonProperty("projectUrl")]
    public string? ProjectUrl { get; set; }

    [JsonProperty("githubUrl")]
    public string? GithubUrl { get; set; }
}
