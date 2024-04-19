using System.Windows.Forms;
using JottyCRM.Core;
using JottyCRM.MVVM.ViewModel;
using JottyCRM.services;

namespace JottyCRM.MVVM.Commands
{
    public class RegisterCommand : BaseCommand
    {
        private readonly RegistrationViewModel _registrationViewModel;
        private readonly UserStore _userStore;
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;

        public RegisterCommand(RegistrationViewModel registrationViewModel, 
            IUserService userService,
            UserStore userStore, 
            INavigationService navigationService)
        {
            _registrationViewModel = registrationViewModel;
            _userService = userService;
            _userStore = userStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            string email = _registrationViewModel.Email;
            string name = _registrationViewModel.Name;
            string login = _registrationViewModel.Login;
            string password = _registrationViewModel.Password;

            UserRegistered registered = _userService.Create(name, login, email, password);

            if (!registered.StatusCode)
            {
                MessageBox.Show(registered.ErrorMessage, "Ошибка при регистрации", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _userStore.CurrentUser = registered.UserObject;
            _navigationService.Navigate();
        }
    }
}