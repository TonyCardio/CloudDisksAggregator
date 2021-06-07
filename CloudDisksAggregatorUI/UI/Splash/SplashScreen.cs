using System;
using System.Drawing;
using System.Windows.Forms;

namespace CloudDisksAggregatorUI.UI.Splash
{
    public partial class SplashScreen : UserControl
    {
        public event Action OnComplete;
        private readonly int[] rainSpeeds = { 4, 6, 8, 3, 5, 6, 7, 4 };
        private readonly Func<bool> until;
        private readonly int loadingSpeed;
        private float initialPercentage;
        private readonly PictureBox[] rainBoxes;

        public SplashScreen(Func<bool> until, int loadingSpeed = 10)
        {
            this.until = until;
            this.loadingSpeed = loadingSpeed;
            InitializeComponent();
            Load += SplashControl_Load;
            rainBoxes = new[]
            {
                pictureBox3, pictureBox4, pictureBox5, pictureBox6,
                pictureBox7, pictureBox8, pictureBox9, pictureBox10
            };
        }

        private void SplashControl_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < rainBoxes.Length; i++)
            {
                var picture = rainBoxes[i];
                picture.Location = new Point(picture.Location.X, picture.Location.Y + rainSpeeds[i]);
                if (picture.Location.Y > rainPanel.Size.Height + picture.Size.Height)
                {
                    picture.Location = new Point(picture.Location.X, -picture.Size.Height);
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            initialPercentage += loadingSpeed;
            float percentage = initialPercentage / puddlePicture.Height * 100;

            label1.Text = (int)percentage % 100 + " %";

            hidePanel.Location = new Point(hidePanel.Location.X, hidePanel.Location.Y + loadingSpeed);
            if (hidePanel.Location.Y > puddlePicture.Location.Y + puddlePicture.Height)
                label1.Text = "∞";
            if (until())
            {
                timer2.Stop();
                OnComplete?.Invoke();
            }
        }

        public void Reset()
        {
            hidePanel.Location = puddlePicture.Location;
            timer1.Stop();
            timer2.Stop();
            label1.Text = "";
        }
    }
}
