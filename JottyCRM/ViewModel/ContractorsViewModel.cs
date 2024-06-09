using System.Collections.Generic;
using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Models.Contractor;
using JottyCRM.Services;
using JottyCRM.Stores;

namespace JottyCRM.ViewModel
{
    public class ContractorsViewModel : BaseViewModel
    {
        public readonly IContractorService _contractorService;
        private readonly UserStore _userStore;
        private readonly ContractorsStore _contractorsStore;
        public List<Contractor> ContractorsList => _contractorService.GetAll(_userStore.CurrentUser.Id);

        public List<UserPropertyContractor> UserProperties =>
            _contractorService.GetUserProperties(_userStore.CurrentUser.Id);

        public ICommand NavigateToCreateContractorCommand { get; }
        public ICommand DeleteContractor { get; set; }

        public ContractorsViewModel(IContractorService contractorService, 
            UserStore userStore,
            ContractorsStore contractorsStore,
            INavigationService createContractorNavigationService)
        {
            _contractorService = contractorService;
            _userStore = userStore;
            _contractorsStore = contractorsStore;

            _contractorsStore.ContractorCreated += OnContractorAdded;

            NavigateToCreateContractorCommand = new NavigateCommand(createContractorNavigationService);
        }

        private void OnContractorAdded(string fullName, string email, string phoneNumber)
        {
            _contractorService.Create(fullName, email, phoneNumber, _userStore.CurrentUser);
        }
    }
}