using System;
using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.View;

namespace JottyCRM.ViewModel
{
    public class CreateSellViewModel : BaseWindowViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _contractorId;
        public int ContractorId
        {
            get => _contractorId;
            set
            {
                _contractorId = value;
                OnPropertyChanged(nameof(ContractorId));
            }
        }

        private Decimal _amountOfTransaction;
        public Decimal AmountOfTransaction
        {
            get => _amountOfTransaction;
            set
            {
                _amountOfTransaction = value;
                OnPropertyChanged(nameof(AmountOfTransaction));
            }
        }

        private DateTime _sellDateTime;

        public DateTime SellDateTime
        {
            get => _sellDateTime;
            set
            {
                _sellDateTime = value;
                OnPropertyChanged(nameof(SellDateTime));
            }
        }
        
        public ICommand CreateSellCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public CreateSellViewModel(UserStore userStore,
            ISellService sellService,
            INavigationService navigationService,
            CloseWindowNavigationService closeWindowNavigationService
            )
        {
            SellDateTime = DateTime.Now.Date;
            CreateSellCommand = new CreateSellCommand(this,
                userStore, navigationService, sellService);
            CloseWindowCommand = new CloseWindowCommand(closeWindowNavigationService);
            var createSellWindow = new CreateSellView()
            {
                DataContext = this
            };
            
            createSellWindow.ShowDialog();
        }
    }
}