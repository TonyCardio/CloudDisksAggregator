using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using CloudDisksAggregator.Clouds;
using CloudDisksAggregator.CloudWrappers;
using CloudDisksAggregatorUI.UI.ViewEntity;

namespace CloudDisksAggregatorUI.UI.ViewControls
{
    public partial class AuthCloudControl : UserControl
    {
        private readonly ICloudDriveWrapper driveWrapper;
        public event Action<ICloudDrive, DriveViewInfo> AuthSucceeded;
        private readonly ChromiumWebBrowser browser;
        private readonly Regex tokenPattern = new Regex("access_token=(.+?)&[t|e]");
        private string cloudToken;

        public AuthCloudControl(ICloudDriveWrapper driveWrapper)
        {
            Name = "AuthCloudControl";
            InitializeComponent();
            this.driveWrapper = driveWrapper;
            Dock = DockStyle.Fill;
            browser = new ChromiumWebBrowser(driveWrapper.AuthUrl);
            Controls.Add(browser);
            browser.Name = "browser";
            browser.Dock = DockStyle.Fill;
            browser.AddressChanged += ParseToken;
            Disposed += AuthCloudControl_Disposed;
        }

        private void ParseToken(object sender, AddressChangedEventArgs e)
        {
            var token = tokenPattern.Match(e.Address);
            if (token.Success)
            {
                Invoke(new Action(() =>
               {
                   Controls.Remove(browser);
                   cloudToken = token.Groups[1].Value;
                   setNamePanel.Show();
               }));
            }
        }

        private void AuthCloudControl_Disposed(object sender, EventArgs e)
        {
            browser.Dispose();
        }

        private void setNameBtn_Click(object sender, EventArgs e)
        {
            AuthSucceeded(
                driveWrapper.CreateCloudDrive(cloudToken),
                new DriveViewInfo(driveWrapper.DriveType, cloudNameBox.Text));
        }
    }
}
