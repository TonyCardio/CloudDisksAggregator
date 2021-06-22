using CloudDisksAggregator.Core;
using CloudDisksAggregator.UI;
using CloudDisksAggregatorUI.FileContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.UI
{
    public partial class MainForm : Form
    {
        private List<UserAccount> accounts;
        private readonly ICloudApi[] apis;
        private readonly IViewerFactory viewerFactory;

        public MainForm(ICloudApi[] apis, IViewerFactory viewerFactory)
        {
            this.apis = apis;
            this.viewerFactory = viewerFactory;
            accounts = apis.SelectMany(x => x.Drive.LoadAccounts()).ToList();
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
            var account = (UserAccount)((Button)sender).Tag;
            controlPanel.Controls.Add(new CloudContentControl(new[] { account }, viewerFactory));
        }

        private void OnAllButton_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            controlPanel.Controls
                .Add(new CloudContentControl(accounts, viewerFactory));
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
            accounts.Add(account);
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