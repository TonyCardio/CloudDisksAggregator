using CloudDisksAggregator.Core;
using CloudDisksAggregatorUI.FileContent;
using CloudDisksAggregatorUI.UI.ContextMenu;
using CloudDisksAggregatorUI.UI.Splash;
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
        private readonly ItemContextMenu itemContextMenu;
        private string currentDirectory;
        private ICloudDriveEngine currentDriveEngine;

        public CloudContentControl(IEnumerable<ICloudDriveEngine> cloudDriveEngines,
            IViewerFactory viewerFactory)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            AllowDrop = true;

            this.viewerFactory = viewerFactory;
            this.cloudDriveEngines = cloudDriveEngines.ToList();
            itemContextMenu = new ItemContextMenu();

            AddItemsFromAllDrives();

            currentDirectory = "/";
            Name = "CloudContentControl";

            viewContentList.ItemActivate += (s, e) => ItemClick();
            viewContentList.MouseUp += ViewContentList_MouseUp;
            DragEnter += CloudContentControl_DragEnter;
            DragDrop += CloudContentControl_DragDrop;
        }

        #region Upload

        private void CloudContentControl_DragDrop(object sender, DragEventArgs e)
        {
            var file = ((string[])e.Data.GetData(DataFormats.FileDrop)).First();
            if (currentDirectory == "/") UploadForAllDrives(file);
            else UploadFile(file);
        }

        private void CloudContentControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.All;
        }

        private async void UploadForAllDrives(string filePath)
        {
            await ShowSplashScreen(
                 Task.WhenAll(cloudDriveEngines.Select(x => x.Upload(filePath, ""))));
        }

        private async void UploadFile(string filePath)
        {
            await ShowSplashScreen(
                 currentDriveEngine.Upload(filePath, currentDirectory));
        }

        #endregion

        #region Navigation

        private void ItemClick()
        {
            var driveEntity = (DriveEntityInfo)viewContentList.FocusedItem.Tag;
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
            var btn = (Button)sender;
            var driveEntity = (DriveEntityInfo)btn.Tag;
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
            var task = driveEntity.DriveEngine.Download(driveEntity.FullPath);
            await ShowSplashScreen(task, false);
            var viewer = viewerFactory.Create(driveEntity.Name,
                await task);
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

        #region SplashAnimation

        private async Task ShowSplashScreen(Task task, bool showAllAfter = true)
        {
            var screen = new SplashControl(() => task.IsCompleted);
            screen.OnComplete += () => CloseSplashScreen(showAllAfter);
            HideAll();
            Controls.Add(screen);
            screen.SizeChange();
            screen.Show();

            try
            {
                await task;
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    e.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CloseSplashScreen(bool showAllAfter)
        {
            Controls.RemoveByKey("SplashControl");
            if (showAllAfter)
                ShowAll();
        }

        #endregion

        #region ContextMenu

        private void ViewContentList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (viewContentList.FocusedItem.Bounds.Contains(e.Location)) ShowItemMenu();
            }
        }

        private void ShowItemMenu()
        {
            var driveEntity = (DriveEntityInfo)viewContentList.FocusedItem.Tag;
            itemContextMenu.Show(driveEntity, Cursor.Position);
        }

        #endregion

        private async void AddItems(string catalogPath, ICloudDriveEngine driveEngine)
        {
            viewContentList?.Items.Clear();
            var catalogEntity = new DriveEntityInfo(catalogPath, driveEngine);
            if (currentDirectory != catalogPath)
                AddFolderBtn(catalogEntity);
            var task = driveEngine.GetCatalogContent(catalogPath);
            await ShowSplashScreen(task);
            var items = await task;
            viewContentList?.Items.AddRange(items.Select(CreateViewItem).ToArray());
        }

        private async void AddItemsFromAllDrives(string catalogPath = "/")
        {
            viewContentList?.Items.Clear();
            var catalogEntity = new DriveEntityInfo(catalogPath, null);
            if (currentDirectory != catalogPath)
                AddFolderBtn(catalogEntity);
            var task = Task.Run(async () =>
            {
                foreach (var engine in cloudDriveEngines)
                {
                    var items = await engine.GetCatalogContent(catalogPath);
                    viewContentList?.Items.AddRange(items.Select(CreateViewItem).ToArray());
                }
            });
            await ShowSplashScreen(task);
        }

        private static ListViewItem CreateViewItem(DriveEntityInfo driveEntity)
        {
            var item = new ListViewItem(
                new string[] { driveEntity.Name },
                driveEntity.Expansion == "Dir" ? 1 : 0,
                SystemColors.ButtonFace, Color.Empty,
                new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point))
            { Tag = driveEntity };
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