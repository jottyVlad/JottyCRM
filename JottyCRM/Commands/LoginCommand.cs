using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.ViewModel;

namespace JottyCRM.Commands
{
    public class LoginCommand : BaseCommand
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly UserStore _userStore;
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;

        public LoginCommand(LoginViewModel loginViewModel, 
            IUserService userService,
            UserStore userStore, 
            INavigationService navigationService)
        {
            _loginViewModel = loginViewModel;
            _userService = userService;
            _userStore = userStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            string login = _loginViewModel.Login;
            string password = _loginViewModel.Password;
            
            UserAuthorized authorizedStatus = _userService.TryAuthorize(login, password);

            if (!authorizedStatus.StatusCode)
            {
                _loginViewModel.Error = "Неверный логин или пароль!";
                return;
            }

            _userStore.CurrentUser = authorizedStatus.UserObject;
            _navigationService.Navigate();
        }
    }
}