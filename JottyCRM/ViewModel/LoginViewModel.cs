using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.View;

namespace JottyCRM.ViewModel
{
    public class LoginViewModel : BaseWindowViewModel
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

        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand CloseWindowCommand { get; }
        
        public LoginViewModel(UserStore userStore, IUserService userService, 
            INavigationService homeNavigationService,
            CloseWindowNavigationService closeNavigationService)
        {
            LoginCommand = new LoginCommand(this, userService, userStore, homeNavigationService);
            CloseWindowCommand = new CloseWindowCommand(this, closeNavigationService);
            
            var loginWindow = new LoginView()
            {
                DataContext = this
            };
            loginWindow.ShowDialog();
        }
        
        
    }
}