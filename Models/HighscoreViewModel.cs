using ProjectCarsSeasonExtension.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace ProjectCarsSeasonExtension.ViewModels
{
    // --------------------------------------------------------------------------------------------

    public class HighscoreViewModel
    {
        public ICollectionView Tracks { get; set; }
        public ICollectionView Player { get; set; }

        public HighscoreViewModel()
        {
            // create some test data
            Random rand = new Random();
            //var _tracks = new List<TrackModel>();
            //int iRand = rand.Next(1, 6);
            //for (var i = 0; i < iRand; i++)
            //{
            //TrackModel trackModel = new TrackModel
            //{
            //TrackName = "Track " + iRand
            //};

            var _player = new List<PlayerModel>();
            int jRand = rand.Next(0, 10);
            for (var j = 0; j < jRand; j++)
            {
                PlayerModel playerModel = new PlayerModel
                {
                    Name = "Test Fahrer " + j,
                    Time = new DateTime()
                };
                //trackModel.PlayerList.Add(playerModel);
                _player.Add(playerModel);
            }
            Player = CollectionViewSource.GetDefaultView(_player);
            //_tracks.Add(trackModel);
            //}
            //Tracks = CollectionViewSource.GetDefaultView(_tracks);
        }
    }

    // --------------------------------------------------------------------------------------------
}
