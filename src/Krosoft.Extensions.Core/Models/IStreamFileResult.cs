namespace Krosoft.Extensions.Core.Models;

public interface IStreamFileResult
{
    Stream Stream { get; }
    string FileName { get; }
    string ContentType { get; }
}