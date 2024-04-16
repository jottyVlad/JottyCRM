using System.Windows.Input;
using JottyCRM.Core;
using JottyCRM.MVVM.ViewModel;
using JottyCRM.services;

namespace JottyCRM.MVVM.Commands
{
    public class NavigateCommand : BaseCommand
    {
        private readonly INavigationService _navigationService;

        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        
        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}