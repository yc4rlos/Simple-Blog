namespace Blog.Infrastructure.Options;

public class AzureOptions
{
    public required string AccountName { get; set; }
    public required string BlobStorageConnectionString { get; set; }
}