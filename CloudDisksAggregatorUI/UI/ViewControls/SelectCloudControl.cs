using CloudDisksAggregatorUI.UI.ViewEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.UI.ViewControls
{
    public partial class SelectCloudControl : UserControl
    {
        public event Action<DriveViewInfo, UserControl> AcceptUserChoice;

        public SelectCloudControl(IEnumerable<DriveViewInfo> viewInfo)
        {
            InitializeComponent();
            foreach(var info in viewInfo)
                AddCloudViewItem(info);
            Name = "SelectCloudControl";
            Dock = DockStyle.Fill;
        }

        private void AcceptCloudDrive(object sender, EventArgs e)
        {
            AcceptUserChoice((DriveViewInfo)((Button)sender).Tag, this);
        }
    }
}
