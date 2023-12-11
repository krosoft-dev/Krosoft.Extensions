namespace Krosoft.Extensions.Pdf.Interfaces;

public interface IPdfService
{
    Stream Create(IEnumerable<string> contents);
    Stream Merge(IEnumerable<Stream> streamsPdf);
    byte[] Merge(IEnumerable<byte[]> streamsPdf);
}