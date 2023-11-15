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

    public static async Task<FileStreamResult> ToFileStreamResult(this Task<IStreamFileResult> task)
    {
        var file = await task;
        return file.ToFileStreamResult();
    }

    //public static FileStreamResult ToFileStreamResult<T>(this CsvStreamFile<T> csvStreamFile)
    //{
    //    var memoryStream = csvStreamFile.ToMemoryStream();
    //    return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = csvStreamFile.FileName.Sanitize() };
    //}
}