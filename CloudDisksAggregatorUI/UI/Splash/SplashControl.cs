using System;
using System.Drawing;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.UI.Splash
{
    public partial class SplashControl : UserControl
    {
        public event Action OnComplete;
        private readonly SplashScreen splashScreen;
        public SplashControl(Func<bool> until)
        {
            splashScreen = new SplashScreen(until);
            splashScreen.OnComplete += () => OnComplete?.Invoke();
            InitializeComponent();
            mainPanel.Controls.Add(splashScreen);
            Dock = DockStyle.Fill;
            Load += (o, e) => SizeChange();
            Name = "SplashControl";
        }

        public void SizeChange()
        {
            splashScreen.Location = new Point((mainPanel.Width - splashScreen.Width) / 2,
                                                (mainPanel.Height - splashScreen.Height) / 2);
        }

        public void Reset() => splashScreen.Reset();
    }
}
