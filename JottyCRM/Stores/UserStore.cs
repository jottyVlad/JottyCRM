using System;
using JottyCRM.Models;

namespace JottyCRM.Core
{
    public class UserStore
    {
        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                CurrentUserChanged?.Invoke();
            }
        }

        public bool isLoggedIn => CurrentUser != null;
        public event Action CurrentUserChanged;

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}