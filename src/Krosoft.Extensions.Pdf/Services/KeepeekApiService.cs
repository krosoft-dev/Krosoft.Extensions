﻿using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Core.Tools;
using Krosoft.Extensions.Pdf.Interfaces;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace Krosoft.Extensions.Pdf.Services;

public class PdfService : IPdfService
{
    public Stream Merge(IEnumerable<Stream> streams)
    {
        Guard.IsNotNull(nameof(streams), streams);
        var output = new MemoryStream();

        if (streams.Any())
        {
            try
            {
                using (var pdf = new PdfDocument())
                {
                    foreach (var stream in streams)
                    {
                        using (var from = PdfReader.Open(stream, PdfDocumentOpenMode.Import))
                        {
                            foreach (var page in from.Pages)
                            {
                                pdf.AddPage(page);
                            }
                        }
                    }

                    pdf.Save(output);
                }
            }
            finally
            {
                foreach (var stream in streams)
                {
                    stream.Dispose();
                }
            }
        }

        return output;
    }

    public byte[] Merge(IEnumerable<byte[]> files)
    {
        Guard.IsNotNull(nameof(files), files);

        var streams = files.ToStreams();
        var stream = Merge(streams);
        return stream.ToByte();
    }

    public Stream Create(IEnumerable<string> contents)
    {
        var output = new MemoryStream();
        using (var document = new PdfDocument())
        {
            foreach (var content in contents)
            {
                var page = document.AddPage();

                var gfx = XGraphics.FromPdfPage(page);

                var font = new XFont("Arial", 20);

                var textColor = XBrushes.Black;
                var layout = new XRect(20, 20, page.Width, page.Height);
                var format = XStringFormats.Center;

                gfx.DrawString(content, font, textColor, layout, format);
            }

            document.Save(output);
        }

        return output;
    }
}