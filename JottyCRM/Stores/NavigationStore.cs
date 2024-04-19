using System;
using JottyCRM.ViewModel;

namespace JottyCRM.Core
{
    public interface INavigationStore
    {
        BaseViewModel CurrentViewModel { set; }
    }
    public class NavigationStore : INavigationStore
    {
        public event Action CurrentViewModelChanged;

        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}