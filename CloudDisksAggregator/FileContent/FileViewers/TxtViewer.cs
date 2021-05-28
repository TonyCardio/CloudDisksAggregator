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
    public partial class TxtViewer : FileViewer
    {
        private readonly IContentReader<string> reader;

        public TxtViewer(IContentReader<string> reader)
        {
            this.reader = reader;
            InitializeComponent();
            Name = "FileViewer";
        }

        public override bool CanView(string fileName) =>
            fileName.EndsWith(".txt");

        protected override void InitializeContent(byte[] bytes)
        {
            var textBox = new TextBox
            {
                ScrollBars = ScrollBars.Both,
                Dock = DockStyle.Fill,
                WordWrap = false,
                Multiline = true,
                ReadOnly = true
            };
            textBox.Font = new Font(textBox.Font.FontFamily, 15);
            textBox.Text = reader.FromBytes(bytes);
            contentPanel.Controls.Add(textBox);
        }
    }
}
