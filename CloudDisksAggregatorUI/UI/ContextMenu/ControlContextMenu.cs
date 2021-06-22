using System;
using System.Drawing;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.UI.ContextMenu
{
    public class ControlContextMenu : ContextMenuStrip
    {
        public event Action OnUpdate;

        #region MenuItems

        private readonly ToolStripMenuItem updateItem = new ToolStripMenuItem("Update");

        #endregion

        public ControlContextMenu()
        {
            BackColor = Color.FromArgb(35, 32, 39);
            RenderMode = ToolStripRenderMode.System;
            ForeColor = SystemColors.ButtonHighlight;
            Items.Add(updateItem);
            InitItems();
        }

        private void InitItems()
        {
            foreach (ToolStripItem toolStripItem in Items) toolStripItem.ForeColor = SystemColors.ButtonHighlight;
            updateItem.Click += (s, e) => OnUpdate?.Invoke();
        }
    }
}
