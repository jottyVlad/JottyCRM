using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.services;

namespace JottyCRM.ViewModel
{
    public class WelcomeViewModel : BaseViewModel
    {
        public string Name => "WelcomeViewModel";

        public ICommand NavigateLoginCommand { get; }
        public ICommand NavigateRegistrationCommand { get; }

        public WelcomeViewModel(INavigationService loginNavigationService, 
            INavigationService registrationNavigationService)
        {
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            NavigateRegistrationCommand = new NavigateCommand(registrationNavigationService);
        }
    }
}