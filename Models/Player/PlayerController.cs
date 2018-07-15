using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectCarsSeasonExtension.Models.Player
{
    public class PlayerController : BaseModel
    {
        public event Action PlayerSelectionChanged; 

        public ObservableCollection<Player> Players { get; set; }

        public bool IsAnyPlayerSelected => SelectedPlayer != null;

        public Player SelectedPlayer
        {
            get
            {
                return _selectedPlayer;
            }

            set
            {
                _selectedPlayer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsAnyPlayerSelected));
                PlayerSelectionChanged?.Invoke();
            }
        }

        private Player _selectedPlayer;
        private readonly DataView _dataView;

        public PlayerController(DataView dataView)
        {
            Players = dataView.Players;
            _dataView = dataView;
        }

        public void AddPlayer(Player newPlayer)
        {
            var maxId = 0;
            if (Players.Count > 0)
                maxId = Players.Max(p => p.Id);

            newPlayer.Id = ++maxId;

            Players.Add(newPlayer);
            
            SelectedPlayer = newPlayer;
        }

        public void RemovePlayerByName(string playerName)
        {
            _dataView.RemovePlayerByName(playerName);
        }

        public void LogoutCurrentPlayer()
        {
            SelectedPlayer = null;
        }

        public bool DoesPlayerExist(string nameOfPlayerToRemove)
        {
            return Players.Any(p => p.Name == nameOfPlayerToRemove);
        }
    }
}
