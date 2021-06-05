using System;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.FileContent.FileViewers
{
    public partial class FileViewer : UserControl
    {
        public event Action OnClose;

        public FileViewer()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        protected virtual void CloseBtnClick(object sender, EventArgs e)
        {
            Hide();
            OnClose?.Invoke();
            contentPanel.Controls.Clear();
        }

        public void InitializeContent(string fileName, byte[] bytes)
        {
            fileNameLable.Text = fileName;
            InitializeContent(bytes);
        }

        public virtual bool CanView(string fileName) => false;

        protected virtual void InitializeContent(byte[] bytes) { }
    }
}
