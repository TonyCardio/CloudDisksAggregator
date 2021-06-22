using CloudDisksAggregator.Core;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.UI.ContextMenu
{
    public class ItemContextMenu : ContextMenuStrip
    {
        public event Action<string, DriveEntityInfo> OnRename;
        public event Action OnGetLink;
        public event Action<DriveEntityInfo> OnDelete;
        public event Action<DriveEntityInfo> OnDownload;

        private DriveEntityInfo item;

        #region MenuItems

        private readonly ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
        private readonly ToolStripMenuItem renameItem = new ToolStripMenuItem("Rename");
        private readonly ToolStripMenuItem getLinkItem = new ToolStripMenuItem("Get link");
        private readonly ToolStripMenuItem downloadItem = new ToolStripMenuItem("Download");
        private readonly ToolStripTextBox newNameBox = new ToolStripTextBox()
        {
            Name = "NameBox",
            BackColor = Color.FromArgb(35, 32, 39)
        };

        #endregion

        public ItemContextMenu()
        {
            BackColor = Color.FromArgb(35, 32, 39);
            RenderMode = ToolStripRenderMode.System;
            ForeColor = SystemColors.ButtonHighlight;
            Items.AddRange(new ToolStripItem[] { deleteItem, downloadItem, getLinkItem, renameItem, newNameBox });
            InitItems();
        }

        public void Show(DriveEntityInfo item, Point location)
        {
            this.item = item;
            newNameBox.Text = Path.GetFileNameWithoutExtension(item.Name);
            Items.Remove(newNameBox);
            Show(location);
        }

        private void InitItems()
        {
            foreach (ToolStripItem toolStripItem in Items) toolStripItem.ForeColor = SystemColors.ButtonHighlight;
            deleteItem.Click += (s, e) => OnDelete?.Invoke(item);
            getLinkItem.Click += (s, e) => OnGetLink?.Invoke();
            downloadItem.Click += (s, e) => OnDownload?.Invoke(item);
            renameItem.Click += RenameItem_Click;
            newNameBox.KeyPress += NewNameBox_KeyPress;
        }

        private void NewNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Items.Remove(newNameBox);
                OnRename?.Invoke($"{newNameBox.Text}.{item.Expansion}", item);
            }
        }

        private void RenameItem_Click(object sender, EventArgs e)
        {
            if (Items.Contains(newNameBox))
                return;
            Items.Add(newNameBox);
            Show(Location);
        }
    }
}
