using System;
using System.Windows.Forms;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregator.UI
{
    public partial class AddNewCloudControl : UserControl
    {
        public event Action<UserAccount> AddingSucceeded;

        public AddNewCloudControl()
        {
            InitializeComponent();
        }

        public void AddChildAddingControl(ICustomAddingControl control)
        {
            control.AddingSucceeded += OnAddingSucceeded;
            Controls.Add((UserControl) control); //TODO : very bad(( fix it!
        }

        private void OnAddingSucceeded(UserAccount obj)
        {
            AddingSucceeded?.Invoke(obj);
        }
    }
}