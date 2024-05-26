using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Services;

namespace JottyCRM.ViewModel
{
    public class ProfileViewModel : BaseViewModel
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

        private readonly UserStore _userStore;
        public ICommand NavigateToChangePassword { get; set; }
        
        public ProfileViewModel(UserStore userStore,
            INavigationService changePasswordNavigationService)
        {
            Name = userStore.CurrentUser.Name;
            Login = userStore.CurrentUser.Login;
            Email = userStore.CurrentUser.Email;

            NavigateToChangePassword = new NavigateCommand(changePasswordNavigationService);
        }
    }
}