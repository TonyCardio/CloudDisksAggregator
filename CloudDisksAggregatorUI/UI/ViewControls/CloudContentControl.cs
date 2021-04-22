using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudDisksAggregator;
using CloudDisksAggregator.CloudDrives;
using CloudDisksAggregator.CloudEngines;

namespace CloudDisksAggregatorUI.UI.ViewControls
{
    public partial class CloudContentControl : UserControl
    {
        private readonly ICloudDriveEngine cloudDriveEngine;

        public CloudContentControl(ICloudDriveEngine cloudDriveEngine)
        {
            Dock = DockStyle.Fill;
            this.cloudDriveEngine = cloudDriveEngine;
            AddItems();
            InitializeComponent();
            Name = "CloudContentControl";
        }

        public async void AddItems()
        {
            var items = await cloudDriveEngine.GetCatalogContent();
            viewContentList.Items.AddRange(items.Select(CreateViewItem).ToArray());
        }

        private ListViewItem CreateViewItem(DriveEntityInfo driveEntity)
        {
            var item = new ListViewItem(
                new string[] { driveEntity.Name },
                driveEntity.Expansion == "Dir" ? 1 : 0,
                SystemColors.ButtonFace, Color.Empty,
              new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point));
            item.Tag = driveEntity;
            return item;
        }
    }
}
