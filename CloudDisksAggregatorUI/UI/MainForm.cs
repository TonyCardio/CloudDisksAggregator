using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CloudDisksAggregator;
using CloudDisksAggregator.CloudDrives;
using CloudDisksAggregator.CloudEngines;
using CloudDisksAggregatorInfrastructure.InMemoryStorage;
using CloudDisksAggregatorUI.Infrastructure;
using CloudDisksAggregatorUI.UI.ViewControls;
using CloudDisksAggregatorUI.UI.ViewEntity;

namespace CloudDisksAggregatorUI.UI
{
    public partial class MainForm : Form
    {
        private readonly ICloudDriveSelector cloudDriveSelector;
        private readonly IInMemoryStorage<DriveViewInfo, ICloudDriveEngine> repository;

        public MainForm(
            ICloudDriveSelector cloudDriveSelector,
            IInMemoryStorage<DriveViewInfo, ICloudDriveEngine> inMemoryStorage)
        {
            InitializeComponent();
            InitView();
            repository = inMemoryStorage;
            this.cloudDriveSelector = cloudDriveSelector;
        }

        #region View

        private Control[] optionPanels;

        private void InitView()
        {
            optionPanels = new[] {yaOptionPanel, dropOptionPanel, addDiskSelectPanel};
        }

        private void HideAllPanels()
        {
            HideAuthControl();
            RemoveControlByKey("CloudContentControl");
            RemoveControlByKey("SelectCloudControl");
            foreach (var panel in optionPanels)
                panel.Hide();
        }

        private void ShowCloudContent(DriveViewInfo viewInfo, UserControl control)
        {
            controlPanel.Controls.Remove(control);
            controlPanel.Controls.Add(new CloudContentControl(repository.Get(viewInfo)));
        }

        private void yaDiskBtn_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            InitCloudsSeletcControl(CloudDriveType.YandexDisk);
            yaOptionPanel.Show();
        }

        private void dropBtn_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            InitCloudsSeletcControl(CloudDriveType.Dropbox);
            dropOptionPanel.Show();
        }

        private void addNewBtn_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            addDiskSelectPanel.Show();
        }

        private void InitCloudsSeletcControl(CloudDriveType driveType)
        {
            var selectContol = new SelectCloudControl(
                repository.GetAllElements()
                    .Select(pair => pair.Item1)
                    .Where(info => info.DriveType == driveType));
            selectContol.AcceptUserChoice += ShowCloudContent;
            controlPanel.Controls.Add(selectContol);
        }

        #endregion

        #region Auth

        private void SaveDrive(ICloudDriveEngine cloudDriveEngine, DriveViewInfo driveViewInfo)
        {
            repository.Add(driveViewInfo, cloudDriveEngine);
            HideAllPanels();
        }

        private void AddYandexDisk(object sender, EventArgs e)
        {
            InitAuthControl(CloudDriveType.YandexDisk);
        }

        private void AddDropbox(object sender, EventArgs e)
        {
            InitAuthControl(CloudDriveType.Dropbox);
        }

        private void InitAuthControl(CloudDriveType driveType)
        {
            HideAllPanels();
            if (cloudDriveSelector.TryGetCloudDrive(driveType, out var cloudDrive))
            {
                var authControl = new AuthCloudControl(cloudDrive);
                authControl.AuthSucceeded += SaveDrive;
                controlPanel.Controls.Add(authControl);
            }
        }

        private void HideAuthControl()
        {
            if (controlPanel.Controls.ContainsKey("AuthCloudControl"))
            {
                var authControl = controlPanel.Controls.Find("AuthCloudControl", false).First();
                authControl.Dispose();
                controlPanel.Controls.Remove(authControl);
            }
        }

        #endregion

        private void RemoveControlByKey(string key)
        {
            if (controlPanel.Controls.ContainsKey(key))
                controlPanel.Controls.RemoveByKey(key);
        }
    }
}