using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;


namespace UdemyTranscriptMergerWPF.Services
{
    class PdfExporter
    {
        public static void Export(string filePath, List<(string Title, string Content)> sections)
        {
            using (var writer = new PdfWriter(filePath))
            using (var pdf = new PdfDocument(writer))
            using (var doc = new iText.Layout.Document(pdf))
            {
                foreach (var sec in sections)
                {
                    // Başlık
                    var h = new iText.Layout.Element.Paragraph(sec.Title)
                        .SetFontSize(24)
                        .SetMarginBottom(10)
                        .SetBold();

                    doc.Add(h);

                    // İçerik
                    doc.Add(new Paragraph(sec.Content).SetFontSize(12));

                    // Yeni sayfa
                    doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }
            }
        }
    }
}
