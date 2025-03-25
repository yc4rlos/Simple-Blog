using Azure.Storage.Blobs;

namespace Blog.Infrastructure.Services.Services;

public class FileService(IOptions<AzureOptions> options) : IFileService
{
    private readonly string _containerName = "images";

    public async Task<string> AddFileAsync(string fileName, Stream file)
    {
        var blobClient = GetContainerClient();
        var generatedName = GenerateFileName(fileName);

        await blobClient.UploadBlobAsync(generatedName, file);
        return generatedName;
    }

    public async Task<BlobObject?> GetFileAsync(string fileName)
    {
        var client = GetContainerClient();
        var blobClient = client.GetBlobClient(fileName);
        if (!await blobClient.ExistsAsync()) return null;

        var content = await blobClient.DownloadContentAsync();
        var streamContent = content.Value.Content.ToStream();

        return new BlobObject { Content = streamContent, ContentType = content.Value.Details.ContentType };
    }

    public async Task DeleteFileAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var client = GetContainerClient();
        await client.DeleteBlobIfExistsAsync(fileName, cancellationToken: cancellationToken);
    }

    private string GenerateFileName(string originalName)
    {
        var extension = Path.GetExtension(originalName);
        return Guid.NewGuid().ToString() + extension;
    }

    private BlobContainerClient GetContainerClient()
    {
        return new BlobContainerClient(options.Value.BlobStorageConnectionString, _containerName);
    }
}