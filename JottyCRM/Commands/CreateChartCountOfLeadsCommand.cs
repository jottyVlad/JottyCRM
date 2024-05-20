using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.View;
using JottyCRM.ViewModel;
using MaterialDesignThemes.Wpf;

namespace JottyCRM.Commands
{
    public class CreateChartCountOfLeadsCommand : BaseCommand
    {
        private readonly AnalyticsViewModel _analyticsViewModel;
        private readonly UserStore _userStore;
        private readonly ILeadService _leadService;

        public CreateChartCountOfLeadsCommand(UserStore userStore,
            AnalyticsViewModel analyticsViewModel,
            ILeadService leadService)
        {
            _analyticsViewModel = analyticsViewModel;
            _userStore = userStore;
            _leadService = leadService;
        }
        
        public override void Execute(object parameter)
        {
            ChartViewModel chartViewModel = new ChartViewModel();
            chartViewModel.DataPoints.Clear();
            foreach (DateTime date in CommandsUtils.EachDay(_analyticsViewModel.StartPeriod, _analyticsViewModel.EndPeriod))
            {
                chartViewModel.DataPoints.Add(new DataPoint(date, _leadService.GetCountOfLeadsOnDate(date, _userStore.CurrentUser.Id)));
            }
            
            chartViewModel.ChartVisibility = "Visible";
            chartViewModel.DisplayName = "Кол-во лидов за период";

            ChartView chartView = new ChartView(chartViewModel)
            {
                DataContext = chartViewModel
            };
            chartView.ShowDialog();
        }
        
        
    }
}