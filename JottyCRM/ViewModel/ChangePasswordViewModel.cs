using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.View;

namespace JottyCRM.ViewModel
{
    public class ChangePasswordViewModel : BaseWindowViewModel
    {
        private string _currentPassword;
        public string CurrentPassword
        {
            get => _currentPassword;
            set
            {
                _currentPassword = value;
                OnPropertyChanged(nameof(CurrentPassword));
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        private string _newPasswordConfirmation;
        public string NewPasswordConfirmation
        {
            get => _newPasswordConfirmation;
            set
            {
                _newPasswordConfirmation = value;
                OnPropertyChanged(nameof(NewPasswordConfirmation));
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

        public ICommand ChangePasswordCommand { get; }
        public ICommand CloseWindowCommand { get; }
        
        public ChangePasswordViewModel(UserStore userStore,
            IUserService userService, 
            INavigationService profileNavigationService,
            CloseWindowNavigationService closeNavigationService)
        {
            ChangePasswordCommand = new ChangePasswordCommand(this, userStore, userService, profileNavigationService);
            CloseWindowCommand = new CloseWindowCommand(this, closeNavigationService);
            
            var changePasswordWindow = new ChangePasswordView()
            {
                DataContext = this
            };
            changePasswordWindow.ShowDialog();
        }
    }
}