using System.Collections.ObjectModel;

namespace JottyCRM.ViewModel
{
    public class ChartViewModel : BaseViewModel
    {
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

        public ChartViewModel()
        {
            DataPoints = new ObservableCollection<DataPoint>();
        }
    }
}