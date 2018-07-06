using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        public ObservableCollection<Models.Player.Player> AllPlayers { get; set; } = new ObservableCollection<Models.Player.Player>();

        public string SetPlayerHandicapInMs { get; set; }

        public Models.Player.Player SelectedPlayer { get; set; }

        public bool IsUserAuthenticated { get; set; }

        public Visibility IsSetHandicapRowVisible => IsUserAuthenticated ? Visibility.Visible : Visibility.Collapsed;

        private static readonly Regex Regex = new Regex("[^0-9.-]+");

        private readonly PlayerController _playerController;

        public HandicapView(DataView dataView, PlayerController playerController)
        {
            DataView = dataView;
            DataView.HandicapChanged += DataView_OnHandicapChanged;

            _playerController = playerController;
            _playerController.PlayerSelectionChanged += PlayerController_OnPlayerSelectionChanged;

            InitializeComponent();

            SelectedSeason = DataView.CurrentSeason;
            OnPropertyChanged(nameof(SelectedSeason));

            SetPlayerSeasonHandicaps();
            SetPlayersWithoutHandicaps();

            UpdateUserAuthentication();
        }

        private void UpdateUserAuthentication()
        {
            if (_playerController.IsAnyPlayerSelected &&
                _playerController.SelectedPlayer.Group >= AuthenticationGroup.Moderator)
            {
                IsUserAuthenticated = true;
            }
            else
            {
                IsUserAuthenticated = false;
            }
            OnPropertyChanged(nameof(IsUserAuthenticated));
            OnPropertyChanged(nameof(IsSetHandicapRowVisible));
        }

        private void PlayerController_OnPlayerSelectionChanged()
        {
            UpdateUserAuthentication();
        }

        private void DataView_OnHandicapChanged()
        {
            SetPlayerSeasonHandicaps();
        }

        private void SetPlayerSeasonHandicaps()
        {
            PlayerSeasonHandicaps.Clear();

            foreach (var dataViewHandicap in DataView.Handicaps)
            {
                if (dataViewHandicap.SeasonId != SelectedSeason.Id) continue;

                var player = DataView.GetPlayerById(dataViewHandicap.PlayerId);

                PlayerSeasonHandicaps.Add(new PlayerSeasonHandicap
                {
                    PlayerName = player.Name,
                    HandicapInMs = dataViewHandicap.Handicap.TotalMilliseconds
                });
            }
        }

        public void SetPlayersWithoutHandicaps()
        {
            AllPlayers.Clear();

            foreach (var dataViewPlayer in DataView.Players)
            {
                AllPlayers.Add(dataViewPlayer);
            }

            if (AllPlayers.Count > 0)
            {
                SelectedPlayer = AllPlayers[0];
                OnPropertyChanged(nameof(SelectedPlayer));
            }
        }

        private void ButtonSetHandicap_OnClick(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(SetPlayerHandicapInMs, out var handicapInMs);
            TimeSpan handicapAsTimeSpan = new TimeSpan(0,0,0,0, handicapInMs);
            DataView.SetPlayerHandicap(SelectedPlayer, SelectedSeason, handicapAsTimeSpan);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AllowOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !Regex.IsMatch(text);
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void SeasonSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetPlayerSeasonHandicaps();
            SetPlayersWithoutHandicaps();
        }
    }

    public class PlayerSeasonHandicap
    {
        public string PlayerName { get; set; }
        public double HandicapInMs { get; set; }
    }
}
