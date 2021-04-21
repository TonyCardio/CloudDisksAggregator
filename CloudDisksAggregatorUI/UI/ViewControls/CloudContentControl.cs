using CloudDisksAggregator.Clouds;
using CloudDisksAggregator.Data;
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
    public partial class CloudContentControl : UserControl
    {
        private readonly ICloudDrive cloudDrive;

        public CloudContentControl(ICloudDrive cloudDrive)
        {
            Dock = DockStyle.Fill;
            this.cloudDrive = cloudDrive;
            AddItems();
            InitializeComponent();
            Name = "CloudContentControl";
        }

        public async void AddItems()
        {
            var items = await cloudDrive.GetCatalogContents();
            viewContentList.Items.AddRange(items.Select(CreateViewItem).ToArray());
        }

        private ListViewItem CreateViewItem(DiskEntityInfo diskEntity)
        {
            var item = new ListViewItem(
                new string[] { diskEntity.Name },
                diskEntity.Expansion == "Dir" ? 1 : 0,
                SystemColors.ButtonFace, Color.Empty,
              new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point));
            item.Tag = diskEntity;
            return item;
        }
    }
}
