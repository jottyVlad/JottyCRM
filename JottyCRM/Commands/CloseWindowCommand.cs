using JottyCRM.Services;
using JottyCRM.ViewModel;

namespace JottyCRM.Commands
{
    public class CloseWindowCommand : BaseCommand
    {
        private readonly CloseWindowNavigationService _navigationService;

        public CloseWindowCommand(CloseWindowNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        
        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}