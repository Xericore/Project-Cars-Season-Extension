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

        public PlayerController(ObservableCollection<Player> players)
        {
            Players = players;
        }

        public void AddPlayer(Player newPlayer)
        {
            var maxId = Players.Max(p => p.Id);
            newPlayer.Id = ++maxId;

            Players.Add(newPlayer);
            
            SelectedPlayer = newPlayer;
        }

        public void RemovePlayer(string playerName)
        {
            var playerToRemove = Players.FirstOrDefault(p => p.Name.Equals(playerName, StringComparison.Ordinal));

            if (playerToRemove == null)
            {
                return;
            }

            Players.Remove(playerToRemove);
        }

        public void LogoutCurrentPlayer()
        {
            SelectedPlayer = null;
        }
    }
}
