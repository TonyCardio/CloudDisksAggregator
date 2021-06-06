using CloudDisksAggregatorUI.FileContent.Readers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.FileContent.FileViewers
{
    public partial class ImageViewer : FileViewer
    {
        private readonly IContentReader<Image> reader;
        private Image content;
        private readonly string[] extensions = { ".png", ".jpg", ".bmp" };

        public ImageViewer(IContentReader<Image> reader)
        {
            this.reader = reader;
            InitializeComponent();
            Name = "FileViewer";
        }

        public override bool CanView(string fileName) =>
            extensions.Any(fileName.ToLower().EndsWith);

        protected override void CloseBtnClick(object sender, EventArgs e)
        {
            content.Dispose();
            base.CloseBtnClick(sender, e);
        }

        protected override void InitializeContent(byte[] bytes)
        {
            content = reader.FromBytes(bytes);
            var imgBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = content,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            contentPanel.Controls.Add(imgBox);
        }
    }
}
