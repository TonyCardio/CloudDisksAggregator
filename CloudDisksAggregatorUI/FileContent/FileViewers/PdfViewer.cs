using Apitron.PDF.Controls;
using Apitron.PDF.Rasterizer;
using CloudDisksAggregatorUI.FileContent.Readers;
using System;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.FileContent.FileViewers
{
    public partial class PdfViewer : FileViewer
    {
        private readonly IContentReader<Document> reader;
        private Document content;

        public PdfViewer(IContentReader<Document> reader)
        {
            this.reader = reader;
            InitializeComponent();
            Name = "FileViewer";
        }

        public override bool CanView(string fileName) =>
            fileName.ToLower().EndsWith(".pdf");

        protected override void CloseBtnClick(object sender, EventArgs e)
        {
            content.Dispose();
            base.CloseBtnClick(sender, e);
        }

        protected override void InitializeContent(byte[] bytes)
        {
            content = reader.FromBytes(bytes);
            var pdfViewer = new PDFViewer
            {
                Document = content,
                Dock = DockStyle.Fill
            };
            contentPanel.Controls.Add(pdfViewer);
        }
    }
}
