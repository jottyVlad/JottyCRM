using System;
using System.Linq;
using System.Windows;
using JottyCRM.Core;

namespace JottyCRM.Services
{
    public class CloseWindowNavigationService : INavigationService
    {
        private readonly WindowNavigationStore _navigationStore;

        public CloseWindowNavigationService(WindowNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }
        
        public void Navigate()
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            window?.Close();
            _navigationStore.Close();
        }
    }
}