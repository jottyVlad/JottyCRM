using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity.Infrastructure;
using JottyCRM.Commands;
using JottyCRM.Models.Contractor;
using JottyCRM.View.Utils;
using JottyCRM.ViewModel;

namespace JottyCRM.View
{
    public partial class ContractorsView : UserControl
    {
        public ContractorsView()
        {
            InitializeComponent();
        }
        
        void OnListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Handled)
                return;

            ListViewItem item = MyVisualTreeHelper.FindParent<ListViewItem>((DependencyObject)e.OriginalSource);
            if (item == null)
                return;

            if (item.Focusable && !item.IsFocused)
                item.Focus();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var t = TableList.SelectedItem as Contractor;
            var ctx = DataContext as ContractorsViewModel;
            ctx.DeleteContractor = new DeleteContractorCommand(ctx._contractorService, t);

            try
            {
                ctx.DeleteContractor.Execute(null);
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Невозможно удалить контрагента, так как " +
                                "у него есть связанные продажи!", "Ошибка удаления", 
                    MessageBoxButton.OK);
                return;
            }
           

            TableList.SelectedItem = null;
            ctx.ContractorsList.Remove(t);

            TableList.ItemsSource = ctx.ContractorsList;
            TableList.Items.Refresh();
        }
    }
}