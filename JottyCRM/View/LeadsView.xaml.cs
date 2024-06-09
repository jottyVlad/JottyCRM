using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Models.Contractor;
using JottyCRM.Models.Lead;
using JottyCRM.View.Utils;
using JottyCRM.ViewModel;

namespace JottyCRM.View
{
    public partial class LeadsView : UserControl
    {
        public LeadsView()
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
            var t = TableList.SelectedItem as Lead;
            var ctx = DataContext as LeadsViewModel;
            ctx.DeleteLead = new DeleteLeadCommand(ctx._leadService, t);
            ctx.DeleteLead.Execute(null);

            TableList.SelectedItem = null;
            ctx.LeadsList.Remove(t);

            TableList.ItemsSource = ctx.LeadsList;
            TableList.Items.Refresh();
        }
    }
}