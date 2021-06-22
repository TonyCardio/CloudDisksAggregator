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
        private readonly ControlContextMenu controlContextMenu;
        private string currentDirectory;
        private ICloudDriveEngine currentDriveEngine;
        private readonly Dictionary<ICloudDriveEngine, string> driveNames;
        private ListView currentListView;
        private Dictionary<ICloudDriveEngine, ListView> listViews = new Dictionary<ICloudDriveEngine, ListView>();
        private Dictionary<ICloudDriveEngine, FlowLayoutPanel> folderPanels = new Dictionary<ICloudDriveEngine, FlowLayoutPanel>();

        public CloudContentControl(IEnumerable<UserAccount> userAccounts,
            IViewerFactory viewerFactory)
        {
            this.viewerFactory = viewerFactory;
            itemContextMenu = new ItemContextMenu();
            controlContextMenu = new ControlContextMenu();
            InitializeControl();
            driveNames = userAccounts.ToDictionary(x => x.DriveEngine, x => x.Name);
            cloudDriveEngines = userAccounts.Select(x => x.DriveEngine).ToList();

            components = new System.ComponentModel.Container();
            var resources
                = new System.ComponentModel.ComponentResourceManager(typeof(CloudContentControl));

            foreach (var driveEngine in cloudDriveEngines)
            {
                var (listView, folderPanel) = InitializeDrivePanel(components, resources);
                listViews.Add(driveEngine, listView);
                folderPanels.Add(driveEngine, folderPanel);
                listViews[driveEngine].ItemActivate += ViewContentList_ItemActivate;
                listViews[driveEngine].MouseUp += ViewContentList_MouseUp;
                AddItems(driveEngine);
            }
            currentDirectory = "/";
            Name = "CloudContentControl";
        }

        #region Search

        private void SearchBox_Press(object sender, EventArgs e)
        {
            var searcher = (SearchBox)sender;
            var text = searcher.Text;

            foreach (var driveEngine in cloudDriveEngines)
            {
                var controlElements = folderPanels[driveEngine].Controls;
                var directory = (DriveEntityInfo)controlElements[^1].Tag;
                var items = SearchAllMatches(driveEngine, text, directory.FullPath);
                AddItems(items, driveEngine);
            }

        }

        private async Task AddItems(Task<List<DriveEntityInfo>> items, ICloudDriveEngine driveEntity)
        {
            listViews[driveEntity]?.Items.Clear();
            var elements = await items;
            listViews[driveEntity]?.Items.AddRange(elements.Select(CreateViewItem).ToArray());
        }

        private async Task<List<DriveEntityInfo>> SearchAllMatches(ICloudDriveEngine driveEntity, string text, string directory)
        {
            var task = driveEntity?.Search(text, directory);
            await ShowSplashScreen(task);
            return await task;
        }

        #endregion

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

        private void ViewContentList_ItemActivate(object sender, EventArgs e)
        {
            var driveEntity = (DriveEntityInfo)((ListView)sender).FocusedItem.Tag;

            if (driveEntity.IsDirectory)
                ChangeDirectory(driveEntity);
            else
                ShowFileViewer(driveEntity);
        }

        private void ChangeDirectory(DriveEntityInfo driveEntity)
        {
            if (driveEntity.Name == "")
                AddItems(driveEntity.DriveEngine, driveEntity.FullPath);
            else
            {
                AddItems(driveEntity.DriveEngine, driveEntity.FullPath);
                currentDriveEngine = driveEntity.DriveEngine;
            }
            currentDirectory = driveEntity.FullPath;
        }

        private void DirectoryBtn_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var driveEntity = (DriveEntityInfo)btn.Tag;
            RemoveAfter(btn, driveEntity.DriveEngine, currentDirectory != driveEntity.FullPath);
            ChangeDirectory(driveEntity);
        }

        private void RemoveAfter(Control control, ICloudDriveEngine driveEntity, bool itself)
        {
            var index = folderPanels[driveEntity].Controls.IndexOf(control);
            if (!itself) index += 1;
            var count = folderPanels[driveEntity].Controls.Count;
            for (var i = count - 1; i >= index; i--) folderPanels[driveEntity].Controls.RemoveAt(i);
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
            currentListView = (ListView)sender;
            if (e.Button == MouseButtons.Right)
            {
                if (currentListView.FocusedItem.Bounds.Contains(e.Location))
                    ShowItemMenu((DriveEntityInfo)currentListView.FocusedItem.Tag);
                else
                    controlContextMenu.Show(Cursor.Position);
            }
        }

        private void ShowItemMenu(DriveEntityInfo driveEntity)
        {
            itemContextMenu.Show(driveEntity, Cursor.Position);
        }

        private void InitControlContextMenu()
        {
            controlContextMenu.OnUpdate += () =>
                AddItems(((DriveEntityInfo)currentListView.FocusedItem.Tag).DriveEngine, currentDirectory);
        }

        #endregion

        private async void AddItems(ICloudDriveEngine driveEngine, string catalogPath = "/")
        {
            listViews[driveEngine]?.Items.Clear();
            var catalogEntity = new DriveEntityInfo(catalogPath, driveEngine);
            if (currentDirectory != catalogPath)
                AddFolderBtn(catalogEntity, driveNames[driveEngine]);
            var task = Task.Run(async () =>
            {
                var items = await driveEngine.GetCatalogContent(catalogPath);
                listViews[driveEngine]?.Items.AddRange(items.Select(CreateViewItem).ToArray());
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