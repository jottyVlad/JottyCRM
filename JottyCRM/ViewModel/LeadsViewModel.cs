using System;
using System.Collections.Generic;
using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Models;
using JottyCRM.Models.Contractor;
using JottyCRM.Models.Lead;
using JottyCRM.repositories;
using JottyCRM.Services;
using JottyCRM.Stores;

namespace JottyCRM.ViewModel
{
    public class LeadsViewModel : BaseViewModel
    {
        private readonly ILeadService _leadService;
        private readonly UserStore _userStore;
        private readonly LeadsStore _leadsStore;
        public List<Lead> LeadsList => _leadService.GetAll(_userStore.CurrentUser.Id);

        public ICommand NavigateToCreateLeadCommand { get; }

        public LeadsViewModel(ILeadService leadService, 
            UserStore userStore,
            LeadsStore leadsStore,
            INavigationService createLeadNavigationService)
        {
            _leadService = leadService;
            _userStore = userStore;
            _leadsStore = leadsStore;

            _leadsStore.LeadCreated += OnLeadAdded;

            NavigateToCreateLeadCommand = new NavigateCommand(createLeadNavigationService);
        }

        private void OnLeadAdded(string name, string status, string description, DateTime createdAt)
        {
            _leadService.Create(name, status, description, createdAt, _userStore.CurrentUser);
        }
    }
}