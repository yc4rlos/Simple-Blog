using Blog.Domain.Exceptions;

namespace Blog.Application.Core.UseCases.Shared.Exceptions.File;

public class FileNotFoundException(string fileName) 
    : NotFoundException($"File with name {fileName} not found");