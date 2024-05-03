using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.Stores;

namespace JottyCRM.ViewModel
{
    public class MainViewModel : BaseWindowViewModel
    {
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand CloseWindowCommand { get; }
        
        public MainViewModel(NavigationStore navigationStore,
            CloseWindowNavigationService closeNavigationService)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            CloseWindowCommand = new CloseWindowCommand(this, closeNavigationService);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}