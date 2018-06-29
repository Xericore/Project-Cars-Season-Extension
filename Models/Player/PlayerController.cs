﻿using System;
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
            if(players.Count <= 0)
                players.Add(new Player { Id = -1, Name = "New player" });

            Players = players;
        }

        public void AddPlayer(Player newPlayer)
        {
            var maxId = Players.Max(p => p.Id);
            newPlayer.Id = ++maxId;

            Players.Add(newPlayer);

            MoveAddPlayerToEnd();

            SelectedPlayer = newPlayer;
        }

        private void MoveAddPlayerToEnd()
        {
            Player addPlayer = Players.First(p => p.Id == -1);

            if (addPlayer == null) return;

            Players.Remove(addPlayer);
            Players.Add(addPlayer);
        }

        public void SetSelectedPlayerAvatar(string newPlayerAvatarFileName)
        {
            SelectedPlayer.AvatarFileName = newPlayerAvatarFileName;
            OnPropertyChanged(nameof(IsAnyPlayerSelected));
            PlayerSelectionChanged?.Invoke();
        }
    }
}
