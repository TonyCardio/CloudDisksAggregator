using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using CloudDisksAggregatorUI.AuthAppData;

namespace CloudDisksAggregatorUI.ViewControls
{
    public partial class AuthCloudControl : UserControl
    {
        private readonly IAuthData authData;
        public event Action<UserControl, string> AuthSucceeded;
        private readonly ChromiumWebBrowser browser;
        private readonly Regex tokenPattern = new Regex("access_token=(.+)&[t|e]");

        public AuthCloudControl(IAuthData authData)
        {
            Name = "AuthCloudControl";
            InitializeComponent();
            this.authData = authData;
            Dock = DockStyle.Fill;
            browser = new ChromiumWebBrowser(authData.AuthUrl);
            Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            browser.AddressChanged += ParseToken;
            Disposed += AuthCloudControl_Disposed;
        }

        private void ParseToken(object sender, AddressChangedEventArgs e)
        {
            var token = tokenPattern.Match(e.Address);
            if (token.Success)
                AuthSucceeded(this, token.Groups[1].Value);
        }

        private void AuthCloudControl_Disposed(object sender, EventArgs e)
        {
            browser.Dispose();
            Dispose();
        }
    }
}
