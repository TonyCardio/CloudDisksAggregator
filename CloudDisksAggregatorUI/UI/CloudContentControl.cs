using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregatorUI.UI
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

        private async void AddItems()
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
