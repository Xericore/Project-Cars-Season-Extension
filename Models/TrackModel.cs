﻿using System.Collections.Generic;

namespace ProjectCarsSeasonExtension.Models
{
    public class TrackModel : BaseModel
    {
        // ----------------------------------------------------------------------------------------

        public string TrackName;
        public string Description;

        private List<PlayerModel> _playerList;

        // ----------------------------------------------------------------------------------------
        // getter and setter
        // ----------------------------------------------------------------------------------------

        public List<PlayerModel> PlayerList
        {
            get { return _playerList; }
            set
            {
                _playerList = value;
                NotifyPropertyChanged("Player");
            }
        }

        // ----------------------------------------------------------------------------------------
    }
}
