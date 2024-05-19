using System;
using System.Collections.Generic;
using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Models.Sell;
using JottyCRM.Services;
using JottyCRM.Stores;

namespace JottyCRM.ViewModel
{
    public class SellsViewModel : BaseViewModel
    {
        private readonly ISellService _sellService;
        private readonly UserStore _userStore;
        private readonly SellsStore _sellsStore;

        public List<Sell> SellsList => _sellService.GetAll(_userStore.CurrentUser.Id);
        
        public ICommand NavigateToCreateSellCommand { get; set; }

        public SellsViewModel(ISellService sellService,
            UserStore userStore,
            SellsStore sellsStore,
            INavigationService createSellNavigationService)
        {
            _sellService = sellService;
            _userStore = userStore;
            _sellsStore = sellsStore;

            NavigateToCreateSellCommand = new NavigateCommand(createSellNavigationService);
            _sellsStore.SellCreated += OnSellCreated;
        }

        private void OnSellCreated(string name, int contractorId, DateTime sellDateTime, Decimal amountOfTransaction)
        {
            _sellService.Create(name, contractorId, sellDateTime, amountOfTransaction, _userStore.CurrentUser);
        }
    }
}