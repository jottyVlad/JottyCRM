using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.ViewModel;

namespace JottyCRM.Commands
{
    public class CreateContractorCommand : BaseCommand
    {
        private readonly CreateContractorViewModel _createContractorViewModel;
        private readonly UserStore _userStore;
        private readonly INavigationService _navigationService;
        private readonly IContractorService _contractorService;

        public CreateContractorCommand(CreateContractorViewModel createContractorViewModel,
            UserStore userStore,
            INavigationService navigationService,
            IContractorService contractorService)
        {
            _createContractorViewModel = createContractorViewModel;
            _userStore = userStore;
            _navigationService = navigationService;
            _contractorService = contractorService;
        }
        
        public override void Execute(object parameter)
        {
            if (_createContractorViewModel.FullName == null)
            {
                return;
            }

            _contractorService.Create(_createContractorViewModel.FullName,
                _createContractorViewModel.Email, _createContractorViewModel.PhoneNumber,
                _userStore.CurrentUser);
            _navigationService.Navigate();
        }
    }
}