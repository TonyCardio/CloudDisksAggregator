using CloudDisksAggregator.Core;
using CloudDisksAggregator.UI;
using CloudDisksAggregatorUI.FileContent;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.UI
{
    public partial class MainForm : Form
    {
        private readonly UserAccount[] accounts;
        private readonly ICloudApi[] apis;
        private readonly IViewerFactory viewerFactory;

        public MainForm(ICloudApi[] apis, IViewerFactory viewerFactory)
        {
            this.apis = apis;
            this.viewerFactory = viewerFactory;
            accounts = apis.SelectMany(x => x.Drive.LoadAccounts()).ToArray();
            InitializeComponent();
            InitView();
        }

        #region View

        private Control[] optionPanels;

        private void InitView()
        {
            optionPanels = new Control[] {addDiskSelectPanel, helpPanel};
        }

        private void OnSelectDriveButton_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            var engine = (ICloudDriveEngine) ((Button) sender).Tag;
            controlPanel.Controls.Add(new CloudContentControl(new[] {engine}, viewerFactory));
        }

        private void OnAllButton_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            controlPanel.Controls
                .Add(new CloudContentControl(accounts.Select(x => x.DriveEngine), viewerFactory));
        }

        private void HideAllPanels()
        {
            HideAddNewControl();
            RemoveControlByKey("CloudContentControl");
            RemoveControlByKey("SelectCloudControl");
            foreach (var panel in optionPanels)
                panel.Hide();
        }

        private void OnAddNewButton_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            addDiskSelectPanel.Show();
        }

        #endregion

        #region AddNew

        private void OnAddNewDrive(UserAccount account)
        {
            AddNewDiskSelectButton(account);
            HideAllPanels();
        }

        private void OnNewDriveButton_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            var drive = (ICloudDriveObject) ((Button) sender).Tag;
            var addDriveControl = new AddNewCloudControl {Dock = DockStyle.Fill};
            controlPanel.Controls.Add(addDriveControl);
            var addingCloudEventHandler = drive.AddNewAccount(addDriveControl);
            addingCloudEventHandler.AddingSucceeded += OnAddNewDrive;
        }

        private void HideAddNewControl()
        {
            if (controlPanel.Controls.ContainsKey("AddNewCloudControl"))
            {
                var addNewControl = controlPanel.Controls.Find("AddNewCloudControl", false).First();
                addNewControl.Dispose();
                controlPanel.Controls.Remove(addNewControl);
            }
        }

        #endregion

        private void OnHelpButton_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            helpPanel.Show();
        }

        private void RemoveControlByKey(string key)
        {
            if (controlPanel.Controls.ContainsKey(key))
                controlPanel.Controls.RemoveByKey(key);
        }
    }
}