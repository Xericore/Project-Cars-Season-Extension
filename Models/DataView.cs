﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Properties;
using ProjectCarsSeasonExtension.Serialization;

namespace ProjectCarsSeasonExtension.Models
{
    public class DataView
    {
        public event Action PlayerRemoved;

        public ObservableCollection<PlayerResult> PlayerResults { get; }
        public ObservableCollection<Player.Player> Players { get; set; }

        public Season CurrentSeason { get; }
        public ObservableCollection<PlayerHandicap> Handicaps { get; }
        public ObservableCollection<Season> Seasons { get; set; }
        public ObservableCollection<Challenge> AllChallenges { get; set; }

        public DataView(ISeasonReader seasonReader)
        {            
            Seasons = seasonReader.GetSeasons();
            if (Seasons.Count > Settings.Default.CurrentSeasonId)
                CurrentSeason = Seasons[Settings.Default.CurrentSeasonId];
            AllChallenges = seasonReader.GetChallenges();
            PlayerResults = seasonReader.GetPlayerResults();
            Players = seasonReader.GetPlayers();
            Handicaps = seasonReader.GetPlayerHandicaps();
        }

        public TimeSpan GetPlayerHandicap(Player.Player player)
        {
            var foundPlayer = Handicaps.FirstOrDefault(p => p.PlayerId == player.Id);

            return foundPlayer?.Handicap ?? new TimeSpan(0);
        }

        public void AddSeason(Season newSeason)
        {
            int maxSeasonId = 0;
            if (Seasons.Count > 0)
            {
                maxSeasonId = Seasons.Max(s => s.Id) + 1;
            }
            newSeason.Id = maxSeasonId;
            Seasons?.Add(newSeason);
        }

        public void RemoveSeason(Season season)
        {
            Seasons?.Remove(season);
        }

        public Challenge GetChallengeById(int challengeId)
        {
            return CurrentSeason?.GetChallengeById(challengeId);
        }

        public void AddChallenge(Challenge challenge)
        {
            AllChallenges.Add(challenge);
        }

        public void RemoveChallenge(Challenge challenge)
        {
            AllChallenges.Remove(challenge);
        }

        public bool AddChallengeResult(int playerId, ChallengeResult challengeResult)
        {
            if (DateTime.Now > CurrentSeason.EndDate)
                return false;

            Challenge foundChallenge = CurrentSeason.Challenges.FirstOrDefault
            (
                c => c.TrackName == challengeResult.TrackLocationAndVariant &&
                     c.CarName == challengeResult.CarName
            );

            if (foundChallenge == null)
                return false;

            PlayerResult foundPlayerResult = PlayerResults.FirstOrDefault(p => p.ChallengeId == foundChallenge.Id && p.PlayerId == playerId);

            if (foundPlayerResult == null)
            {
                PlayerResults.Add(new PlayerResult
                {
                    PlayerId = playerId,
                    ChallengeId = foundChallenge.Id,
                    FastestLap = challengeResult.LastValidLapTime
                });
            }
            else
            {
                if (challengeResult.LastValidLapTime < foundPlayerResult.FastestLap)
                    foundPlayerResult.FastestLap = challengeResult.LastValidLapTime;
            }

            return true;
        }

        public void RemovePlayerByName(string playerName)
        {
            var playerToRemove = Players.FirstOrDefault(p => p.Name.Equals(playerName, StringComparison.Ordinal));

            if (playerToRemove == null)
                return;

            Players.Remove(playerToRemove);

            foreach (var playerResult in PlayerResults.Where(p => p.PlayerId == playerToRemove.Id).ToList())
            {
                PlayerResults.Remove(playerResult);
            }

            foreach (var handicap in Handicaps.Where(p => p.PlayerId == playerToRemove.Id).ToList())
            {
                Handicaps.Remove(handicap);
            }

            PlayerRemoved?.Invoke();
        }

        public Player.Player GetPlayerById(int playerId)
        {
            return Players.FirstOrDefault(p => p.Id == playerId);
        }
    }
}
