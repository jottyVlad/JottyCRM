using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.View;

namespace JottyCRM.ViewModel
{
    public class RegistrationViewModel : BaseWindowViewModel
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

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
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

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        
        public ICommand RegisterCommand { get; }
        public new ICommand CloseWindowCommand { get; }

        public RegistrationViewModel(UserStore userStore, IUserService userService,
            INavigationService homeNavigationService,
            CloseWindowNavigationService closeNavigationService)
        {
            RegisterCommand = new RegisterCommand(this, userService, userStore, homeNavigationService);
            CloseWindowCommand = new CloseWindowCommand(closeNavigationService);
            
            var registrationWindow = new RegistrationView()
            {
                DataContext = this
            };
            registrationWindow.ShowDialog();
        }
    }
}