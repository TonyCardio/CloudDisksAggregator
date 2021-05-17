using System;
using System.Linq;
using System.Windows.Forms;
using CloudDisksAggregator.Core;
using CloudDisksAggregator.UI;

namespace CloudDisksAggregatorUI.UI
{
    public partial class MainForm : Form
    {
        private readonly UserAccount[] accounts;
        private readonly ICloudApi[] apis;

        public MainForm(ICloudApi[] apis)
        {
            this.apis = apis;
            accounts = apis.SelectMany(x => x.Drive.LoadAccounts()).ToArray();
            InitializeComponent();
            InitView();
        }

        #region View

        private Control[] optionPanels;

        private void InitView()
        {
            optionPanels = new[] {addDiskSelectPanel};
        }

        private void OnSelectDriveButton_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            var engine = (ICloudDriveEngine) ((Button) sender).Tag;
            controlPanel.Controls.Add(new CloudContentControl(engine));
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
            addDriveControl.AddingSucceeded += OnAddNewDrive;
            controlPanel.Controls.Add(addDriveControl);
            drive.AddNewAccount(addDriveControl);
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

        private void RemoveControlByKey(string key)
        {
            if (controlPanel.Controls.ContainsKey(key))
                controlPanel.Controls.RemoveByKey(key);
        }
    }
}