using System;
using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.View;

namespace JottyCRM.ViewModel
{
    public class CreateLeadViewModel : BaseWindowViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private DateTime _createdAt;

        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                _createdAt = value;
                OnPropertyChanged(nameof(CreatedAt));
            }
        }
        
        public ICommand CreateLeadCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public CreateLeadViewModel(UserStore userStore,
            ILeadService leadService,
            INavigationService navigationService,
            CloseWindowNavigationService closeWindowNavigationService
            )
        {
            CreateLeadCommand = new CreateLeadCommand(this,
                userStore, navigationService, leadService);
            CloseWindowCommand = new CloseWindowCommand(this, closeWindowNavigationService);
            var createLeadWindow = new CreateLeadView()
            {
                DataContext = this
            };
            
            createLeadWindow.ShowDialog();
        }
    }
}