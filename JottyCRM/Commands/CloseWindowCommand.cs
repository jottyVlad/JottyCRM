using JottyCRM.Services;
using JottyCRM.ViewModel;

namespace JottyCRM.Commands
{
    public class CloseWindowCommand : BaseCommand
    {
        private readonly BaseWindowViewModel _loginViewModel;
        private readonly CloseWindowNavigationService _navigationService;

        public CloseWindowCommand(BaseWindowViewModel loginViewModel, CloseWindowNavigationService navigationService)
        {
            _loginViewModel = loginViewModel;
            _navigationService = navigationService;
        }
        
        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}