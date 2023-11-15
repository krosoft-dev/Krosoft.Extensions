using CsvHelper;
using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Reporting.Csv.Helpers;
using Krosoft.Extensions.Reporting.Csv.Models;

namespace Krosoft.Extensions.Reporting.Csv.Extensions;

public static class CsvFileExtensions
{
    public static MemoryStream ToMemoryStream<T>(this CsvFile<T> csvFile)
    {
        var result = csvFile.ToBytes();
        var memoryStream = new MemoryStream(result);
        return memoryStream;
    }

    public static byte[] ToBytes<T>(this CsvFile<T> csvFile)
    {
        var config = CsvConfigurationHelper.GetCsvConfiguration(csvFile.Culture);
        using (var memoryStream = new MemoryStream())
        using (var streamWriter = new StreamWriter(memoryStream, config.Encoding))
        using (var csvWriter = new CsvWriter(streamWriter, config))
        {
            csvWriter.WriteRecords(csvFile.Data);
            streamWriter.Flush();
            return memoryStream.ToArray();
        }
    }

    public static IStreamFileResult ToCsvStreamResult<T>(this CsvFile<T> csvStreamFile)
    {
        var memoryStream = csvStreamFile.ToMemoryStream();
        return new CsvStreamFileResult(memoryStream, csvStreamFile.FileName);
    }

    public static async Task<IStreamFileResult> ToCsvStreamResult<T>(this Task<CsvFile<T>> task)
    {
        var file = await task;
        return file.ToCsvStreamResult();
    }
}