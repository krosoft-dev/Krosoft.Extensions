using Krosoft.Extensions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Krosoft.Extensions.WebApi.Extensions;

public static class StreamFileResultExtensions
{
    public static FileStreamResult ToFileStreamResult(this IStreamFileResult file)
    {
        var fileStreamResult = new FileStreamResult(file.Stream, file.ContentType) { FileDownloadName = file.FileName };
        return fileStreamResult;
    }

    public static async Task<FileStreamResult> ToFileStreamResult<T>(this Task<T> task) where T : IStreamFileResult
    {
        var file = await task;
        return file.ToFileStreamResult();
    }
}