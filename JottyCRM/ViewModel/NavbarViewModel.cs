using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Services;

namespace JottyCRM.ViewModel
{
    public class NavbarViewModel : BaseViewModel
    {
        public ICommand NavigateProfile { get; set; }
        public ICommand NavigateContractors { get; set; }
        public ICommand NavigateLeads { get; set; }
        public ICommand NavigateSells { get; set; }
        public ICommand NavigateExit { get; set; }
        
        public ICommand NavigateAnalytics { get; set; }

        public NavbarViewModel(INavigationService profileNavigationService,
            INavigationService contractorsNavigationService,
            INavigationService leadsNavigationService,
            INavigationService sellsNavigationService,
            INavigationService analyticsNavigationService)
            // INavigationService exitNavigationService)
        {
            NavigateProfile = new NavigateCommand(profileNavigationService);
            NavigateContractors = new NavigateCommand(contractorsNavigationService);
            NavigateLeads = new NavigateCommand(leadsNavigationService);
            NavigateSells = new NavigateCommand(sellsNavigationService);
            NavigateAnalytics = new NavigateCommand(analyticsNavigationService);
            // NavigateExit = new NavigateCommand(exitNavigationService);
            NavigateExit = new LogoutCommand(); // TODO: change it
        }
    }
}