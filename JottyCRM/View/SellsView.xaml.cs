using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Models.Sell;
using JottyCRM.View.Utils;
using JottyCRM.ViewModel;

namespace JottyCRM.View
{
    public partial class SellsView : UserControl
    {
        public SellsView()
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
            var t = TableList.SelectedItem as Sell;
            var ctx = DataContext as SellsViewModel;
            ctx.DeleteSell = new DeleteSellCommand(ctx._sellService, t);
            ctx.DeleteSell.Execute(null);

            TableList.SelectedItem = null;
            ctx.SellsList.Remove(t);

            TableList.ItemsSource = ctx.SellsList;
            TableList.Items.Refresh();
        }
    }
}