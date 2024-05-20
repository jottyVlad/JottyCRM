using System;
using JottyCRM.Core;
using JottyCRM.Services;
using JottyCRM.View;
using JottyCRM.ViewModel;

namespace JottyCRM.Commands
{
    public class CreateChartRevenueSellsCommand : BaseCommand
    {
        private readonly AnalyticsViewModel _analyticsViewModel;
        private readonly UserStore _userStore;
        private readonly ISellService _sellService;

        public CreateChartRevenueSellsCommand(UserStore userStore,
            AnalyticsViewModel analyticsViewModel,
            ISellService sellService)
        {
            _analyticsViewModel = analyticsViewModel;
            _userStore = userStore;
            _sellService = sellService;
        }
        
        public override void Execute(object parameter)
        {
            ChartViewModel chartViewModel = new ChartViewModel();
            chartViewModel.DataPoints.Clear();
            
            foreach (DateTime date in CommandsUtils.EachDay(_analyticsViewModel.StartPeriod, _analyticsViewModel.EndPeriod))
            {
                chartViewModel.DataPoints.Add(new DataPoint(date, _sellService.GetRevenueOnDate(date, _userStore.CurrentUser.Id)));
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