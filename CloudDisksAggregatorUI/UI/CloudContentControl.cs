using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregatorUI.UI
{
    public partial class CloudContentControl : UserControl
    {
        private readonly List<ICloudDriveEngine> cloudDriveEngines;
        private string currentDirectory;

        public CloudContentControl(IEnumerable<ICloudDriveEngine> cloudDriveEngines)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            this.cloudDriveEngines = cloudDriveEngines.ToList();
            AddItems();
            currentDirectory = "/";
            Name = "CloudContentControl";
            AllowDrop = true;
            viewContentList.ItemActivate += ViewContentList_ItemActivate;
            DragEnter += CloudContentControl_DragEnter;
            DragDrop += CloudContentControl_DragDrop;
        }

        private async void CloudContentControl_DragDrop(object sender, DragEventArgs e)
        {
            var file = ((string[]) e.Data.GetData(DataFormats.FileDrop)).First();
            var tasks = cloudDriveEngines.Select(x => x.Upload(file, currentDirectory == "/" ? "" : currentDirectory));
            await Task.WhenAll(tasks);
            AddItems(currentDirectory);
        }

        private void CloudContentControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.All;
        }

        private void ViewContentList_ItemActivate(object sender, EventArgs e)
        {
            var driveEntity = (DriveEntityInfo) viewContentList.SelectedItems[0].Tag;
            ChangeDirectory(driveEntity);
        }

        private async void AddItems(string catalogPath = "/")
        {
            viewContentList?.Items.Clear();
            var catalogEntity = new DriveEntityInfo(catalogPath);
            if (currentDirectory != catalogPath)
                AddFolderBtn(catalogEntity);
            foreach (var engine in cloudDriveEngines)
            {
                var items = await engine.GetCatalogContent(catalogPath);
                viewContentList.Items.AddRange(items.Select(CreateViewItem).ToArray());
            }
        }

        private void ChangeDirectory(DriveEntityInfo driveEntity)
        {
            if (driveEntity.Expansion == "Dir")
            {
                AddItems(driveEntity.FullPath);
                currentDirectory = driveEntity.FullPath;
            }
        }

        private void DirectoryBtn_Click(object sender, EventArgs e)
        {
            var btn = (Button) sender;
            var driveEntity = (DriveEntityInfo) btn.Tag;
            RemoveAfter(btn, currentDirectory != driveEntity.FullPath);
            ChangeDirectory(driveEntity);
        }

        private void RemoveAfter(Control control, bool itself)
        {
            var index = folderPanel.Controls.IndexOf(control);
            if (!itself) index += 1;
            var count = folderPanel.Controls.Count;
            for (int i = count - 1; i >= index; i--) folderPanel.Controls.RemoveAt(i);
        }

        private ListViewItem CreateViewItem(DriveEntityInfo driveEntity)
        {
            var item = new ListViewItem(
                new string[] {driveEntity.Name},
                driveEntity.Expansion == "Dir" ? 1 : 0,
                SystemColors.ButtonFace, Color.Empty,
                new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point));
            item.Tag = driveEntity;
            return item;
        }
    }
}