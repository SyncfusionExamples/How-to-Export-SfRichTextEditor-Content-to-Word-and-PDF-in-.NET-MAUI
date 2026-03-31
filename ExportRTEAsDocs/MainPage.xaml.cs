using System.Text;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;

namespace ExportRTEAsDocs
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void ExportDoc_Click(object sender, EventArgs e)
        {
            string html = editor?.HtmlText ?? string.Empty;
            if (string.IsNullOrWhiteSpace(html))
            {
                await DisplayAlert("Export", "Editor is empty.", "OK");
                return;
            }

            using var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html));
            using var wordDoc = new WordDocument(htmlStream, FormatType.Html);
            using var outStream = new MemoryStream();
            wordDoc.Save(outStream, FormatType.Docx);
            outStream.Position = 0;

            SaveService saveService = new();
            saveService.SaveAndView("Sample.docx", "application/msword", outStream);
        }

        async void ExportPdf_Click(object sender, EventArgs e)
        {
            string html = editor?.HtmlText ?? string.Empty;
            if (string.IsNullOrWhiteSpace(html))
            {
                await DisplayAlert("Export", "Editor is empty.", "OK");
                return;
            }

            using var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html));
            using var wordDoc = new WordDocument(htmlStream, FormatType.Html);
            using var renderer = new DocIORenderer();
            using PdfDocument pdfDoc = renderer.ConvertToPDF(wordDoc);
            using var outPdf = new MemoryStream();
            pdfDoc.Save(outPdf);
            outPdf.Position = 0;

            SaveService saveService = new();
            saveService.SaveAndView("Sample.pdf", "application/pdf", outPdf);
        }
    }
}
