using Blog.Domain.Abstractions.Services;

namespace Blog.Application.Core.UseCases.File.GetFile;

internal class GetFileQueryHandler(IFileService fileService)
    :IRequestHandler<GetFileQuery, GetFileResult>
{
    public async Task<GetFileResult> Handle(GetFileQuery query, CancellationToken cancellationToken)
    {
        var resp = await fileService.GetFileAsync(query.FileName);
        
        if(resp == null) throw new FileNotFoundException(query.FileName);
        
        return new GetFileResult(resp);
    }
}