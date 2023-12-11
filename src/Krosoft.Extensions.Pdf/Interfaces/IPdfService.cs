namespace Krosoft.Extensions.Pdf.Interfaces;

public interface IPdfService
{ 
    Stream Merge(IEnumerable<Stream> streamsPdf);
    byte[] Merge(IEnumerable<byte[]> streamsPdf);
}