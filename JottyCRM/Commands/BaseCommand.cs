using System;
using System.Windows.Input;

namespace JottyCRM.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public virtual bool CanExecute(object parameter) => true;
        
        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}