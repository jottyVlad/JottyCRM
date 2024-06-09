using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.View;

namespace JottyCRM.ViewModel
{
    public class CreateContractorViewModel : BaseWindowViewModel
    {
        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        public ICommand CreateContractorCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public CreateContractorViewModel(UserStore userStore,
            IContractorService contractorService,
            INavigationService navigationService,
            CloseWindowNavigationService closeWindowNavigationService
            )
        {
            CreateContractorCommand = new CreateContractorCommand(this,
                userStore, navigationService, contractorService);
            CloseWindowCommand = new CloseWindowCommand(closeWindowNavigationService);
            var createContractorWindow = new CreateContractorView()
            {
                DataContext = this
            };
            
            createContractorWindow.ShowDialog();
        }
    }
}