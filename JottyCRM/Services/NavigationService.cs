using System;
using System.Windows.Navigation;
using JottyCRM.Core;
using JottyCRM.Stores;
using JottyCRM.ViewModel;

namespace JottyCRM.Services
{
    public interface INavigationService
    {
        void Navigate();
    }
    
    public class NavigationService<TViewModel> : INavigationService where TViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
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