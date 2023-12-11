using System.Reflection;
using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Core.Helpers;
using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Pdf.Extensions;
using Krosoft.Extensions.Pdf.Interfaces;
using Krosoft.Extensions.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Pdf.Tests.Services;

[TestClass]
public class PdfServiceTests : BaseTest
{
    private IPdfService _pdfService = null!;

    protected override void AddServices(IServiceCollection services,
                                        IConfiguration configuration) => services.AddPdf();

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _pdfService = serviceProvider.GetRequiredService<IPdfService>();
    }

    [TestMethod]
    public void MergeStreamNullTest()
    {
        Check.ThatCode(() => { _pdfService.Merge((IEnumerable<Stream>)null!); })
             .Throws<KrosoftTechniqueException>()
             .WithMessage("La variable 'streams' n'est pas renseignée.");
    }

    [TestMethod]
    public void MergeByteNullTest()
    {
        Check.ThatCode(() => { _pdfService.Merge((IEnumerable<byte[]>)null!); })
             .Throws<KrosoftTechniqueException>()
             .WithMessage("La variable 'files' n'est pas renseignée.");
    }

    [TestMethod]
    public void MergeStreamEmptyTest()
    {
        var stream = _pdfService.Merge(new List<Stream>());

        Check.That(stream).IsNotNull();
    }

    [TestMethod]
    public void MergeByteEmptyTest()
    {
        var stream = _pdfService.Merge(new List<byte[]>());

        Check.That(stream).IsNotNull();
    }

    [TestMethod]
    public void MergeStreamTest()
    {
        var pdf1 = FileHelper.ReadAsStream(Assembly.GetExecutingAssembly(), "sample1.pdf", EncodingHelper.GetEuropeOccidentale());
        Check.That(pdf1).IsNotNull();
        Check.That(pdf1.Length).IsEqualTo(13264);

        var pdf2 = FileHelper.ReadAsStream(Assembly.GetExecutingAssembly(), "sample1.pdf", EncodingHelper.GetEuropeOccidentale());
        Check.That(pdf2).IsNotNull();
        Check.That(pdf2.Length).IsEqualTo(13264);

        var files = new List<Stream>();
        files.Add(pdf1);
        files.Add(pdf2);
        var data = _pdfService.Merge(files);
        FileHelper.CreateFile("Files/sample-stream.pdf", data);

        Check.That(data).IsNotNull();
    }

    [TestMethod]
    public void MergeByteTest()
    {
        var pdf1 = FileHelper.ReadAsStream(Assembly.GetExecutingAssembly(), "sample1.pdf", EncodingHelper.GetEuropeOccidentale()).ToByte();
        var pdf2 = FileHelper.ReadAsStream(Assembly.GetExecutingAssembly(), "sample1.pdf", EncodingHelper.GetEuropeOccidentale()).ToByte();

        var files = new List<byte[]>();
        files.Add(pdf1);
        files.Add(pdf2);
        var data = _pdfService.Merge(files);
        FileHelper.CreateFile("Files/sample-byte.pdf", data);

        Check.That(data).IsNotNull();
    }
}