using Blog.Domain.ValueObjects;

namespace Blog.Domain.Abstractions.Services;

public interface IFileService
{
    
    /// <summary>
    /// Uploads the file and returns the fileName
    /// </summary>
    Task<string> AddFileAsync(string fileName, Stream file);

    /// <summary>
    /// Get the file Stream
    /// </summary>
    Task<BlobObject?> GetFileAsync(string fileName);
    
    /// <summary>
    /// Delete a file
    /// </summary>
    Task DeleteFileAsync(string fileName, CancellationToken cancellationToken = default);
}