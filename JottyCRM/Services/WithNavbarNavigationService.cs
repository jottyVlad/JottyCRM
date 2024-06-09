using System;
using JottyCRM.Stores;
using JottyCRM.ViewModel;

namespace JottyCRM.Services
{
    public class WithNavbarNavigationService<TViewModel> : INavigationService where TViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        private readonly Func<NavbarViewModel> _createNavbarViewModel;

        public WithNavbarNavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel,
            Func<NavbarViewModel> createNavbarViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _createNavbarViewModel = createNavbarViewModel;
        }
        
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new WithNavbarViewModel(_createNavbarViewModel(), _createViewModel());
        }
    }
}