using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JottyCRM.View
{
    public partial class CreateLeadView : Window
    {
        public CreateLeadView()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}