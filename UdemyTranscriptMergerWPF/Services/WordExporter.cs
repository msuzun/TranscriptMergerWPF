using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;


namespace UdemyTranscriptMergerWPF.Services
{
    class WordExporter
    {
        public static void Export(string filePath, List<(string Title, string Content)> sections)
        {
            using (var doc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                var main = doc.AddMainDocumentPart();
                main.Document = new Document(new Body());
                var body = main.Document.Body;

                foreach (var sec in sections)
                {
                    // Başlık
                    var heading = new Paragraph(
                        new Run(new Text(sec.Title))
                    );

                    heading.ParagraphProperties = new ParagraphProperties(
                        new ParagraphStyleId() { Val = "Heading1" }
                    );

                    body.AppendChild(heading);

                    // İçerik
                    var content = new Paragraph(new Run(new Text(sec.Content)));
                    body.AppendChild(content);

                    // Yeni sayfa
                    body.AppendChild(new Paragraph(new Run(new Break() { Type = BreakValues.Page })));
                }
            }
        }
    }
}
