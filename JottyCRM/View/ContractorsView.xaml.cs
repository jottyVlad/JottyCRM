using System.Windows.Controls;
using JottyCRM.ViewModel;

namespace JottyCRM.View
{
    public partial class ContractorsView : UserControl
    {
        public ContractorsView()
        {
            InitializeComponent();

            var _viewModel = this.DataContext as ContractorsViewModel;
            
        }
    }
}