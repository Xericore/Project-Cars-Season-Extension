using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ProjectCarsSeasonExtension.Views.Player
{
    public class NewPlayer: Models.Player.Player, IDataErrorInfo
    {
        private readonly ObservableCollection<Models.Player.Player> _alreadyPresentPlayers;

        public NewPlayer(){ }

        public NewPlayer(ObservableCollection<Models.Player.Player> alreadyPresentPlayers)
        {
            _alreadyPresentPlayers = alreadyPresentPlayers;
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName != "Name") return null;

                if (string.IsNullOrEmpty(Name))
                    result = "Please enter a name.";

                if (_alreadyPresentPlayers.Any(p => p.Name == Name))
                    result = "Player is already present. Please choose a different name.";

                return result;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}