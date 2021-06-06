using CloudDisksAggregator.Core;
using CloudDisksAggregatorUI.FileContent;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.UI
{
    public partial class CloudContentControl : UserControl
    {
        private readonly IViewerFactory viewerFactory;
        private readonly List<ICloudDriveEngine> cloudDriveEngines;
        private string currentDirectory;
        private ICloudDriveEngine currentDriveEngine;

        public CloudContentControl(IEnumerable<ICloudDriveEngine> cloudDriveEngines,
            IViewerFactory viewerFactory)
        {
            this.viewerFactory = viewerFactory;
            InitializeComponent();
            Dock = DockStyle.Fill;
            this.cloudDriveEngines = cloudDriveEngines.ToList();
            AddItemsFromAllDrives();
            currentDirectory = "/";
            Name = "CloudContentControl";
            AllowDrop = true;
            viewContentList.ItemActivate += ViewContentList_ItemActivate;
            DragEnter += CloudContentControl_DragEnter;
            DragDrop += CloudContentControl_DragDrop;
        }

        #region Upload

        private async void CloudContentControl_DragDrop(object sender, DragEventArgs e)
        {
            var file = ((string[])e.Data.GetData(DataFormats.FileDrop)).First();
            if (currentDirectory == "/") await UploadForAllDrives(file);
            else await UploadFile(file);
        }

        private void CloudContentControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.All;
        }

        private async Task UploadForAllDrives(string filePath)
        {
            await Task.WhenAll(cloudDriveEngines.Select(x => x.Upload(filePath, "")));
            AddItemsFromAllDrives(currentDirectory);
        }

        private async Task UploadFile(string filePath)
        {
            await currentDriveEngine.Upload(filePath, currentDirectory);
            AddItems(currentDirectory, currentDriveEngine);
        }

        #endregion

        #region Navigation

        private void ViewContentList_ItemActivate(object sender, EventArgs e)
        {
            var driveEntity = (DriveEntityInfo)viewContentList.SelectedItems[0].Tag;
            if (driveEntity.IsDirectory)
                ChangeDirectory(driveEntity);
            else
                ShowFileViewer(driveEntity);
        }

        private void ChangeDirectory(DriveEntityInfo driveEntity)
        {
            if (driveEntity.Name == "")
                AddItemsFromAllDrives(driveEntity.FullPath);
            else
            {
                AddItems(driveEntity.FullPath, driveEntity.DriveEngine);
                currentDriveEngine = driveEntity.DriveEngine;
            }
            currentDirectory = driveEntity.FullPath;
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
            for (var i = count - 1; i >= index; i--) folderPanel.Controls.RemoveAt(i);
        }

        #endregion

        #region FileView

        private async void ShowFileViewer(DriveEntityInfo driveEntity)
        {
            var driveEngine = driveEntity.DriveEngine;
            var viewer = viewerFactory.Create(driveEntity.Name, await driveEngine.Download(driveEntity.FullPath));
            viewer.OnClose += CloseViewer;
            HideAll();
            Controls.Add(viewer);
            viewer.Show();
        }

        private void CloseViewer()
        {
            Controls.RemoveByKey("FileViewer");
            ShowAll();
        }

        #endregion

        private async void AddItems(string catalogPath, ICloudDriveEngine driveEngine)
        {
            viewContentList?.Items.Clear();
            var catalogEntity = new DriveEntityInfo(catalogPath, driveEngine);
            if (currentDirectory != catalogPath)
                AddFolderBtn(catalogEntity);
            var items = await driveEngine.GetCatalogContent(catalogPath);
            viewContentList?.Items.AddRange(items.Select(CreateViewItem).ToArray());
        }

        private async void AddItemsFromAllDrives(string catalogPath = "/")
        {
            viewContentList?.Items.Clear();
            var catalogEntity = new DriveEntityInfo(catalogPath, null);
            if (currentDirectory != catalogPath)
                AddFolderBtn(catalogEntity);
            foreach (var engine in cloudDriveEngines)
            {
                var items = await engine.GetCatalogContent(catalogPath);
                viewContentList?.Items.AddRange(items.Select(CreateViewItem).ToArray());
            }
        }

        private static ListViewItem CreateViewItem(DriveEntityInfo driveEntity)
        {
            var item = new ListViewItem(
                new string[] {driveEntity.Name},
                driveEntity.Expansion == "Dir" ? 1 : 0,
                SystemColors.ButtonFace, Color.Empty,
                new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point)) {Tag = driveEntity};
            return item;
        }

        private void HideAll()
        {
            foreach (Control control in Controls) control.Hide();
        }

        private void ShowAll()
        {
            foreach (Control control in Controls) control.Show();
        }
    }
}