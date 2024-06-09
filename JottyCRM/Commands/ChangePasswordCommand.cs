using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.ViewModel;

namespace JottyCRM.Commands
{
    public class ChangePasswordCommand : BaseCommand
    {
        private readonly ChangePasswordViewModel _changePasswordViewModel;
        private readonly UserStore _userStore;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        public ChangePasswordCommand(ChangePasswordViewModel changePasswordViewModel,
            UserStore userStore,
            IUserService userService,
            INavigationService profileNavigationService)
        {
            _changePasswordViewModel = changePasswordViewModel;
            _userStore = userStore;
            _userService = userService;
            _navigationService = profileNavigationService;
        }
        
        public override void Execute(object parameter)
        {
            var newUser = _userService.ChangePassword(_userStore.CurrentUser, _changePasswordViewModel.CurrentPassword,
                    _changePasswordViewModel.NewPassword, _changePasswordViewModel.NewPasswordConfirmation);
            if (newUser == null)
                return;
            _userStore.CurrentUser = newUser;
            _navigationService.Navigate();
        }
    }
}