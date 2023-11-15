namespace Krosoft.Extensions.Reporting.Csv.Models
{
    public class CsvFile<T>
    {
        public CsvFile(IEnumerable<T> data, string fileName, string culture)
        {
            Data = data;
            FileName = fileName;
            Culture = culture;
        }

        public IEnumerable<T> Data { get; }
        public string FileName { get; }
        public string Culture { get; }
    }
}