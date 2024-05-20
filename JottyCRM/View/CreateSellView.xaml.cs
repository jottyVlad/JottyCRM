﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using JottyCRM.ViewModel;

namespace JottyCRM.View
{
    public partial class CreateSellView : Window
    {
        public CreateSellView()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();

            var dataContext = this.DataContext as CreateSellViewModel; // TODO: delete it
            var k = 1;
        }

        private void AmountOfTransactionTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;        
        }
    }
}