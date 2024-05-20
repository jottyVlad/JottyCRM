using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using JottyCRM.Commands;
using JottyCRM.Core;
using JottyCRM.Services;

namespace JottyCRM.ViewModel
{
    public class AnalyticsViewModel : BaseViewModel
    {
        private DateTime _startPeriod;

        public DateTime StartPeriod
        {
            get => _startPeriod;
            set
            {
                _startPeriod = value;
                OnPropertyChanged(nameof(StartPeriod));
            }
        }

        private DateTime _endPeriod;

        public DateTime EndPeriod
        {
            get => _endPeriod;
            set
            {
                _endPeriod = value;
                OnPropertyChanged(nameof(EndPeriod));
            }
        }

        public ObservableCollection<DataPoint> DataPoints { get; set; }

        private string _displayName;

        public string DisplayName
        {
            get => _displayName;
            set
            {
                _displayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        private string _chartVisibility;

        public string ChartVisibility
        {
            get => _chartVisibility;
            set
            {
                _chartVisibility = value;
                OnPropertyChanged(nameof(ChartVisibility));
            }
        }
        
        private readonly UserStore _userStore;
        private readonly ILeadService _leadService;
        private readonly ISellService _sellService;

        public ICommand CreateChartCountOfLeads { get; set; }
        public ICommand CreateChartCountOfSells { get; set; }
        public ICommand CreateChartRevenue { get; set; }

        public AnalyticsViewModel(UserStore userStore,
            ILeadService leadService,
            ISellService sellService)
        {
            _userStore = userStore;
            _leadService = leadService;
            _sellService = sellService;

            DataPoints = new ObservableCollection<DataPoint>();

            StartPeriod = DateTime.Now.Date;
            EndPeriod = DateTime.Now.Date;

            ChartVisibility = "Hidden";

            CreateChartCountOfLeads = new CreateChartCountOfLeadsCommand(userStore, this, _leadService);
            CreateChartCountOfSells = new CreateChartCountOfSellsCommand(userStore, this, sellService);
            CreateChartRevenue = new CreateChartRevenueSellsCommand(userStore, this, sellService);
        }
    }

    public class DataPoint {
        public DateTime Date { get; set; }
        public Decimal Value { get; set; }
        
        public DataPoint(DateTime date, Decimal value) {
            Date = date;
            Value = value;
        }
    }
}