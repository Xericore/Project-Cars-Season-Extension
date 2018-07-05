using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for HandicapView.xaml
    /// </summary>
    public partial class HandicapView : Page, INotifyPropertyChanged
    {
        public DataView DataView { get; set; }

        public ObservableCollection<PlayerSeasonHandicap> PlayerSeasonHandicaps { get; set; } = new ObservableCollection<PlayerSeasonHandicap>();

        public Season SelectedSeason { get; set; }

        public HandicapView(DataView dataView)
        {
            DataView = dataView;

            InitializeComponent();

            SelectedSeason = DataView.CurrentSeason;
            OnPropertyChanged(nameof(SelectedSeason));

            SetPlayerSeasonHandicaps();
        }

        private void SetPlayerSeasonHandicaps()
        {
            foreach (var dataViewHandicap in DataView.Handicaps)
            {
                if (dataViewHandicap.SeasonId == SelectedSeason.Id)
                {
                    var player = DataView.GetPlayerById(dataViewHandicap.PlayerId);
                    PlayerSeasonHandicaps.Add(new PlayerSeasonHandicap
                    {
                        PlayerName = player.Name,
                        HandicapInMs = dataViewHandicap.Handicap.TotalMilliseconds
                    });
                }
            }
        }

        private void ButtonAddHandicap_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void DataGrid_OnCurrentCellChanged(object sender, EventArgs e)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class PlayerSeasonHandicap
    {
        public string PlayerName { get; set; }
        public double HandicapInMs { get; set; }
    }
}
