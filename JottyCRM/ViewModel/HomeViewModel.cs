using JottyCRM.Core;

namespace JottyCRM.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;

        public string Name => _userStore.CurrentUser.Name;

        public HomeViewModel(UserStore userStore)
        {
            _userStore = userStore;
        }
    }
}