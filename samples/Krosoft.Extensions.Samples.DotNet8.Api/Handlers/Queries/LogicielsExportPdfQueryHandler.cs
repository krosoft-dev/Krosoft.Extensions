using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Pdf.Interfaces;
using Krosoft.Extensions.Samples.DotNet8.Api.Models.Queries;
using Krosoft.Extensions.Samples.Library.Factories;
using MediatR;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Handlers.Queries;

public class LogicielsExportPdfQueryHandler : IRequestHandler<LogicielsExportPdfQuery, IFileStream>
{
    private readonly ILogger<LogicielsExportPdfQueryHandler> _logger;

    private readonly IPdfService _pdfService;

    public LogicielsExportPdfQueryHandler(ILogger<LogicielsExportPdfQueryHandler> logger, IPdfService pdfService)
    {
        _logger = logger;
        _pdfService = pdfService;
    }

    public async Task<IFileStream> Handle(LogicielsExportPdfQuery request,
                                          CancellationToken cancellationToken)
    {
        _logger.LogInformation("Export des logiciels en PDF...");

        await Task.Delay(2000, cancellationToken);

        var logiciels = LogicielFactory.GetRandom(10)
                                       .AsQueryable()
                                       .Where(x => !string.IsNullOrEmpty(x.Nom))
                                       .Select(x => x.Nom!)
                                       .ToList();

        var steam = _pdfService.Create(logiciels);

        return new PdfFileStream(steam, "Logiciels.pdf");
    }
}