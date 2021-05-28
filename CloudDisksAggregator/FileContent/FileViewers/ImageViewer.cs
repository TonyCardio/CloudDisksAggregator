using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudDisksAggregator.FileContent.Readers;

namespace CloudDisksAggregator.FileContent.FileViewers
{
    public partial class ImageViewer : FileViewer
    {
        private readonly IContentReader<Image> reader;
        private Image content;

        public ImageViewer(IContentReader<Image> reader)
        {
            this.reader = reader;
            InitializeComponent();
            Name = "FileViewer";
        }

        public override bool CanView(string fileName) =>
            new[] { ".png", ".jpg" }.Any(fileName.EndsWith);

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
                Image =content,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            contentPanel.Controls.Add(imgBox);
        }
    }
}
