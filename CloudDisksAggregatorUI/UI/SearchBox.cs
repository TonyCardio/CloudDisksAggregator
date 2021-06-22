using System;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.UI
{
    internal class SearchBox : TextBox
    {
        public event EventHandler Press;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OnEvent();
            else
                base.OnKeyDown(e);
        }

        private void OnEvent() => Press?.Invoke(this, EventArgs.Empty);
    }
}