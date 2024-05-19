using System;
using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.ViewModel;

namespace JottyCRM.Commands
{
    public class CreateSellCommand : BaseCommand
    {
        private readonly CreateSellViewModel _createSellViewModel;
        private readonly UserStore _userStore;
        private readonly INavigationService _navigationService;
        private readonly ISellService _sellService;

        public CreateSellCommand(CreateSellViewModel createSellViewModel,
            UserStore userStore,
            INavigationService navigationService,
            ISellService sellService)
        {
            _createSellViewModel = createSellViewModel;
            _userStore = userStore;
            _navigationService = navigationService;
            _sellService = sellService;
        }
        
        public override void Execute(object parameter)
        {
            _sellService.Create(_createSellViewModel.Name,
                _createSellViewModel.ContractorId,
                _createSellViewModel.SellDateTime,
                _createSellViewModel.AmountOfTransaction,
                _userStore.CurrentUser);
            _navigationService.Navigate();
        }
    }
}