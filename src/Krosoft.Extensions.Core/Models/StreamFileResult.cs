using Krosoft.Extensions.Core.Extensions;

namespace Krosoft.Extensions.Core.Models;

public class StreamFileResult : IStreamFileResult
{
    public StreamFileResult(Stream stream, string fileName, string contentType)
    {
        Stream = stream;
        FileName = fileName.Sanitize();
        ContentType = contentType;
    }

    public Stream Stream { get; }
    public string FileName { get; }
    public string ContentType { get; }
}