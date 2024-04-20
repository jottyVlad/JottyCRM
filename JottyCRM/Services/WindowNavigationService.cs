using System;
using JottyCRM.Core;
using JottyCRM.ViewModel;

namespace JottyCRM.services
{
    public class WindowNavigationService<TViewModel> : INavigationService where TViewModel : BaseViewModel
    {
        private readonly WindowNavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public WindowNavigationService(WindowNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}