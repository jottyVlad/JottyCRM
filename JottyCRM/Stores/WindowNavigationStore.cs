using JottyCRM.ViewModel;

namespace JottyCRM.Core
{
    public class WindowNavigationStore
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
            }
        }
        public void Close()
        {
            CurrentViewModel = null;
        }
    }
}