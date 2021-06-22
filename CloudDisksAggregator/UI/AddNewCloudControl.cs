using System.Windows.Forms;

namespace CloudDisksAggregator.UI
{
    public partial class AddNewCloudControl : UserControl
    {
        public AddNewCloudControl()
        {
            InitializeComponent();
        }

        public void AddChildControl(UserControl control)
        {
            Controls.Add(control);
        }
    }
}