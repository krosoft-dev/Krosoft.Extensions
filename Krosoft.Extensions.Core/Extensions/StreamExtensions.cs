using System.Text;

namespace Krosoft.Extensions.Core.Extensions;

public static class StreamExtensions
{
    private static readonly Encoding DefaultEncoding = new UTF8Encoding(false, true);

    public static BinaryReader CreateReader(this Stream stream) => new BinaryReader(stream, DefaultEncoding, true);

    public static BinaryWriter CreateWriter(this Stream stream) => new BinaryWriter(stream, DefaultEncoding, true);

    public static DateTimeOffset ReadDateTimeOffset(this BinaryReader reader) => new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);

    public static void Write(this BinaryWriter writer, DateTimeOffset value)
    {
        writer.Write(value.Ticks);
    }

    public static byte[] ToByte(this Stream input)
    {
        using (var ms = new MemoryStream())
        {
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }

    public static string ToBase64(this Stream stream)
    {
        if (stream is MemoryStream memoryStream)
        {
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        var bytes = new byte[(int)stream.Length];

        stream.Seek(0, SeekOrigin.Begin);
        stream.Read(bytes, 0, (int)stream.Length);

        return Convert.ToBase64String(bytes);
    }
}