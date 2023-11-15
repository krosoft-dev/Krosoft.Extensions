using System.Net.Mime;

namespace Krosoft.Extensions.Core.Models;

public class ZipStreamFileResult : StreamFileResult
{
    public ZipStreamFileResult(Stream stream,
                               string fileName)
        : base(stream, fileName, MediaTypeNames.Application.Zip)
    {
    }
}