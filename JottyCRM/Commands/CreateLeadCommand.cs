using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.Stores;
using JottyCRM.ViewModel;

namespace JottyCRM.Commands
{
    public class CreateLeadCommand : BaseCommand
    {
        private readonly CreateLeadViewModel _createLeadViewModel;
        private readonly UserStore _userStore;
        private readonly INavigationService _navigationService;
        private readonly LeadsStore _leadsStore;

        public CreateLeadCommand(CreateLeadViewModel createLeadViewModel,
            UserStore userStore,
            INavigationService navigationService,
            LeadsStore leadsStore)
        {
            _createLeadViewModel = createLeadViewModel;
            _userStore = userStore;
            _navigationService = navigationService;
            _leadsStore = leadsStore;
        }
        
        public override void Execute(object parameter)
        {
            if (_createLeadViewModel.Name == null)
            {
                return;
            }

            _leadsStore.CreateLead(_createLeadViewModel.Name,
                _createLeadViewModel.Status, _createLeadViewModel.Description,
                _createLeadViewModel.CreatedAt);
            /*_leadService.Create(_createLeadViewModel.Name,
                _createLeadViewModel.Status, _createLeadViewModel.Description,
                _createLeadViewModel.CreatedAt, _userStore.CurrentUser);*/
            _navigationService.Navigate();
        }
    }
}