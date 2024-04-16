using System.Windows.Input;
using JottyCRM.Core;
using JottyCRM.MVVM.Commands;
using JottyCRM.services;

namespace JottyCRM.MVVM.ViewModel
{
    public class WelcomeViewModel : BaseViewModel
    {
        public string Name => "WelcomeViewModel";

        public ICommand NavigateLoginCommand { get; }

        public WelcomeViewModel(INavigationService loginNavigationService)
        {
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
        }
    }
}