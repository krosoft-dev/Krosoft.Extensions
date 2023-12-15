﻿using Krosoft.Extensions.Core.Helpers;
using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Pdf.Interfaces;
using Krosoft.Extensions.Samples.DotNet8.Api.Models.Queries;
using Krosoft.Extensions.Samples.Library.Factories;
using MediatR;
using System.Text;

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

        var assembly = typeof(AddresseFactory).Assembly;
        var encoding = Encoding.ASCII;

        var pdf1 = AssemblyHelper.ReadAsStream(assembly, "sample1.pdf", encoding);
        var pdf2 = AssemblyHelper.ReadAsStream(assembly, "sample2.pdf", encoding);

        var data = _pdfService.Merge(pdf1, pdf2);

        return new PdfFileStream(data, "Logiciels.pdf");
    }
}