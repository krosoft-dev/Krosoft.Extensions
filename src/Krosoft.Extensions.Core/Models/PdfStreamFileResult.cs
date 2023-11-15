using System.Net.Mime;

namespace Krosoft.Extensions.Core.Models;

public class PdfStreamFileResult : StreamFileResult
{
    public PdfStreamFileResult(Stream stream,
                               string fileName)
        : base(stream, fileName, MediaTypeNames.Application.Pdf)
    {
    }
}