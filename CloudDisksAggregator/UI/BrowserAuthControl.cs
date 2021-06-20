using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.UI
{
    internal partial class BrowserAuthControl : UserControl, IAddingCloudEventHandler
    {
        private readonly Func<string, ICloudDriveEngine> createEngine;
        public event Action<UserAccount> AddingSucceeded;
        private readonly ChromiumWebBrowser browser;
        private readonly Regex tokenPattern = new Regex("access_token=(.+?)&[t|e]");
        private string cloudToken;

        public BrowserAuthControl(string url, Func<string, ICloudDriveEngine> createEngine)
        {
            Name = "AuthCloudControl";
            InitializeComponent();
            this.createEngine = createEngine;
            Dock = DockStyle.Fill;
            browser = new ChromiumWebBrowser(url);
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
            AddingSucceeded?.Invoke(new UserAccount(cloudNameBox.Text, createEngine(cloudToken)));
        }
    }
}