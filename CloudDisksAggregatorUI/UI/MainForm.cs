using System;
using System.Linq;
using System.Windows.Forms;
using CloudDisksAggregatorUI.AuthAppData;
using CloudDisksAggregatorUI.UI.ViewControls;

namespace CloudDisksAggregatorUI.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitView();
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
            foreach (var panel in optionPanels)
                panel.Hide();
        }

        private void yaDiskBtn_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            yaOptionPanel.Show();
        }

        private void dropBtn_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            dropOptionPanel.Show();
        }

        private void addNewBtn_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            addDiskSelectPanel.Show();
        }

        #endregion

        #region Auth

        private void TakeToken(UserControl userControl, string token)
        {
            userControl.Dispose();
        }

        private void AddYandexDisk(object sender, EventArgs e)
        {
            HideAllPanels();
            var authControl = new AuthCloudControl(new YandexAuthData());
            authControl.AuthSucceeded += TakeToken;
            controlPanel.Controls.Add(authControl);
        }

        private void AddDropbox(object sender, EventArgs e)
        {
            HideAllPanels();
            var authControl = new AuthCloudControl(new DropboxAuthData());
            authControl.AuthSucceeded += TakeToken;
            controlPanel.Controls.Add(authControl);
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
    }
}