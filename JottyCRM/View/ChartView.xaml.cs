using System.Windows;
using JottyCRM.ViewModel;

namespace JottyCRM.View
{
    public partial class ChartView : Window
    {
        private readonly ChartViewModel _chartViewModel;
        public ChartView(ChartViewModel chartViewModel)
        {
            _chartViewModel = chartViewModel;
            InitializeComponent();

            Chart.Series[0].IsValueShownAsLabel = true;
            foreach (DataPoint dataPoint in _chartViewModel.DataPoints)
            {
                Chart.Series[0].Points.Add((double)dataPoint.Value).AxisLabel = dataPoint.Date.Date.ToShortDateString();
            }
        }
    }
}