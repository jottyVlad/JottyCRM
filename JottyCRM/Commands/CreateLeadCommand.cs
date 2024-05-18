using JottyCRM.Core;
using JottyCRM.Models.Contractor;
using JottyCRM.Services;
using JottyCRM.ViewModel;

namespace JottyCRM.Commands
{
    public class CreateLeadCommand : BaseCommand
    {
        private readonly CreateLeadViewModel _createLeadViewModel;
        private readonly UserStore _userStore;
        private readonly INavigationService _navigationService;
        private readonly ILeadService _leadService;

        public CreateLeadCommand(CreateLeadViewModel createLeadViewModel,
            UserStore userStore,
            INavigationService navigationService,
            ILeadService leadService)
        {
            _createLeadViewModel = createLeadViewModel;
            _userStore = userStore;
            _navigationService = navigationService;
            _leadService = leadService;
        }
        
        public override void Execute(object parameter)
        {
            if (_createLeadViewModel.Name == null)
            {
                return;
            }

            _leadService.Create(_createLeadViewModel.Name,
                _createLeadViewModel.Status, _createLeadViewModel.Description,
                _createLeadViewModel.CreatedAt, _userStore.CurrentUser);
            _navigationService.Navigate();
        }
    }
}