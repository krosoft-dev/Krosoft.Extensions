namespace Krosoft.Extensions.Core.Models;

public class CsvStreamFileResult : StreamFileResult
{
    public CsvStreamFileResult(Stream stream,
                               string fileName)
        : base(stream, fileName, "text/csv")
    {
    }
}