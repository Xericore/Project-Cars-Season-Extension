using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        public pCarsDataClass MapStructToClass(pCarsAPIStruct pcarsDataStruct, pCarsDataClass pCarsData)
        {
            //pCarsDataClass pCarsData = new pCarsDataClass();
            pCarsData.Version = pcarsDataStruct.Version;
            pCarsData.BuildVersion = pcarsDataStruct.BuildVersion;

            // Session type
            pCarsData.GameState = (GameState)pcarsDataStruct.GameState;
            pCarsData.SessionState = (SessionState)pcarsDataStruct.SessionState;
            pCarsData.RaceState = (RaceState)pcarsDataStruct.RaceState;

            pCarsData.ViewedParticipantIndex = pcarsDataStruct.ViewedParticipantIndex;
            pCarsData.NumParticipants = pcarsDataStruct.NumParticipants;

            if (pCarsData.PlayerParticipantIndex < 0)
            {
                pCarsData.PlayerParticipantIndex = pCarsData.ViewedParticipantIndex;
            }

            for (var loop = 0; loop < (uint)ApiStructLengths.NumParticipants; loop++)
            {
                if (pCarsData.listParticipantInfo.Count != (uint)ApiStructLengths.NumParticipants)
                {
                    for (var i = 0; i < (uint)ApiStructLengths.NumParticipants; i++)
                    {
                        pCarsData.listParticipantInfo.Add(new pCarsParticipantsClass());
                    }
                }

                if (pcarsDataStruct.ParticipantData[loop].CurrentLap != 0)
                {
                    var newPartData = new pCarsParticipantsClass
                    {
                        parIsActive = pcarsDataStruct.ParticipantData[loop].IsActive,
                        parName = pcarsDataStruct.ParticipantData[loop].Name,
                        parWorldPosition = new List<float>(pcarsDataStruct.ParticipantData[loop].WorldPosition),
                        parCurrentLapDistance = pcarsDataStruct.ParticipantData[loop].CurrentLapDistance,
                        parRacePosition = pcarsDataStruct.ParticipantData[loop].RacePosition,
                        parLapsCompleted = pcarsDataStruct.ParticipantData[loop].LapsCompleted,
                        parCurrentLap = pcarsDataStruct.ParticipantData[loop].CurrentLap,
                        parCurrentSector = (CurrentSector)pcarsDataStruct.ParticipantData[loop].CurrentSector
                    };

                    pCarsData.listParticipantInfo[loop] = newPartData;
                }
            }

            // Unfiltered Input
            pCarsData.UnfilteredThrottle = pcarsDataStruct.UnfilteredThrottle;
            pCarsData.UnfilteredBrake = pcarsDataStruct.UnfilteredBrake;
            pCarsData.UnfilteredSteering = pcarsDataStruct.UnfilteredSteering;
            pCarsData.UnfilteredClutch = pcarsDataStruct.UnfilteredClutch;

            // Vehicle & Track information
            pCarsData.CarName = pcarsDataStruct.CarName;
            pCarsData.CarClassName = pcarsDataStruct.CarClassName;
            pCarsData.LapsInEvent = pcarsDataStruct.LapsInEvent;
            pCarsData.TrackLocation = pcarsDataStruct.TrackLocation;
            pCarsData.TrackVariant = pcarsDataStruct.TrackVariation;
            pCarsData.TrackLength = pcarsDataStruct.TrackLength;

            // Timing & Scoring
            pCarsData.LapInvalidated = pcarsDataStruct.LapInvalidated;
            pCarsData.LastLapTime = pcarsDataStruct.LastLapTime;
            pCarsData.CurrentTime = pcarsDataStruct.CurrentTime;

            pCarsData.SplitTimeAhead = pcarsDataStruct.SplitTimeAhead;
            pCarsData.SplitTimeBehind = pcarsDataStruct.SplitTimeBehind;
            pCarsData.SplitTime = pcarsDataStruct.SplitTime;
            pCarsData.EventTimeRemaining = pcarsDataStruct.EventTimeRemaining;


            //make sure that the collections are not empty
            if (pCarsData.CurrentLapTime.Count == 0)
                pCarsData.CurrentLapTime = new ObservableCollection<LapTimesClass> { new LapTimesClass() };

            if (pCarsData.SessionFastestLapTime.Count == 0)
                pCarsData.SessionFastestLapTime = new ObservableCollection<LapTimesClass> { new LapTimesClass() };

            if (pCarsData.PersonalFastestLapTime.Count == 0)
                pCarsData.PersonalFastestLapTime = new ObservableCollection<LapTimesClass> { new LapTimesClass() };

            if (pCarsData.WorldFastestLapTime.Count == 0)
                pCarsData.WorldFastestLapTime = new ObservableCollection<LapTimesClass> { new LapTimesClass() };

            //create the new entry at index 0
            //index 0 is the first in the collection
            //a collection is required for the datagrid binding

            pCarsData.CurrentLapTime[0] = new LapTimesClass
            {
                ltLapTime = pcarsDataStruct.CurrentTime,
                ltSect1 = pcarsDataStruct.CurrentSector1Time,
                ltSect2 = pcarsDataStruct.CurrentSector2Time,
                ltSect3 = pcarsDataStruct.CurrentSector3Time
            };

            pCarsData.SessionFastestLapTime[0] = new LapTimesClass
            {
                ltLapTime = pcarsDataStruct.SessionFastestLapTime,
                ltSect1 = pcarsDataStruct.SessionFastestSector1Time,
                ltSect2 = pcarsDataStruct.SessionFastestSector2Time,
                ltSect3 = pcarsDataStruct.SessionFastestSector3Time
            };

            pCarsData.PersonalFastestLapTime[0] = new LapTimesClass
            {
                ltLapTime = pcarsDataStruct.PersonalFastestLapTime,
                ltSect1 = pcarsDataStruct.PersonalFastestSector1Time,
                ltSect2 = pcarsDataStruct.PersonalFastestSector2Time,
                ltSect3 = pcarsDataStruct.PersonalFastestSector3Time
            };

            pCarsData.WorldFastestLapTime[0] = new LapTimesClass
            {
                ltLapTime = pcarsDataStruct.WorldFastestLapTime,
                ltSect1 = pcarsDataStruct.WorldFastestSector1Time,
                ltSect2 = pcarsDataStruct.WorldFastestSector2Time,
                ltSect3 = pcarsDataStruct.WorldFastestSector3Time
            };

            // Flags
            pCarsData.FlagColour = (FlagColors)pcarsDataStruct.HighestFlagColour;
            pCarsData.FlagReason = (FlagReason)pcarsDataStruct.HighestFlagReason;

            // Pit Info
            pCarsData.PitMode = (PitMode)pcarsDataStruct.PitMode;
            pCarsData.PitSchedule = (PitSchedule)pcarsDataStruct.PitSchedule;

            // Car State
            pCarsData.CarFlags = (CarFlags)pcarsDataStruct.CarFlags;
            pCarsData.OilTempCelsius = pcarsDataStruct.OilTempCelsius;
            pCarsData.OilPressureKPa = pcarsDataStruct.OilPressureKPa;
            pCarsData.WaterTempCelsius = pcarsDataStruct.WaterTempCelsius;
            pCarsData.WaterPressureKPa = pcarsDataStruct.WaterPressureKPa;
            pCarsData.FuelPressureKPa = pcarsDataStruct.FuelPressureKPa;

            pCarsData.FuelLevel = (float)Math.Round(pcarsDataStruct.FuelLevel * pcarsDataStruct.FuelCapacity, 2);
            pCarsData.FuelCapacity = pcarsDataStruct.FuelCapacity;
            pCarsData.Speed = pcarsDataStruct.Speed;
            pCarsData.RPM = pcarsDataStruct.RPM;
            pCarsData.MaxRPM = pcarsDataStruct.MaxRPM;

            pCarsData.Brake = pcarsDataStruct.Brake;
            pCarsData.Throttle = pcarsDataStruct.Throttle;
            pCarsData.Clutch = pcarsDataStruct.Clutch;
            pCarsData.Steering = pcarsDataStruct.Steering;
            pCarsData.Gear = pcarsDataStruct.Gear;
            pCarsData.NumGears = pcarsDataStruct.NumGears;
            pCarsData.OdometerKM = pcarsDataStruct.OdometerKM;
            pCarsData.AntiLockActive = pcarsDataStruct.AntiLockActive;

            pCarsData.LastOpponentCollisionIndex = pcarsDataStruct.LastOpponentCollisionIndex;
            pCarsData.LastOpponentCollisionMagnitude = pcarsDataStruct.LastOpponentCollisionMagnitude;

            pCarsData.BoostActive = pcarsDataStruct.BoostActive;
            pCarsData.BoostAmount = pcarsDataStruct.BoostAmount;

            // Motion & Device Related
            //////pCarsData.WorldPosition = new List<float>(pcarsDataStruct.WorldPosition);
            pCarsData.Orientation = new List<float>(pcarsDataStruct.Orientation);
            pCarsData.LocalVelocity = new List<float>(pcarsDataStruct.LocalVelocity);
            pCarsData.WorldVelocity = new List<float>(pcarsDataStruct.WorldVelocity);
            pCarsData.AngularVelocity = new List<float>(pcarsDataStruct.AngularVelocity);
            pCarsData.LocalAcceleration = new List<float>(pcarsDataStruct.LocalAcceleration);
            pCarsData.WorldAcceleration = new List<float>(pcarsDataStruct.WorldAcceleration);
            pCarsData.ExtentsCentre = new List<float>(pcarsDataStruct.ExtentsCentre);

            // Wheels / Tyres
            pCarsData.TyreFlags = new List<uint>(pcarsDataStruct.TyreFlags);
            pCarsData.Terrain = new List<uint>(pcarsDataStruct.Terrain);
            pCarsData.TyreY = new List<float>(pcarsDataStruct.TyreY);
            pCarsData.TyreRPS = new List<float>(pcarsDataStruct.TyreRPS);
            pCarsData.TyreSlipSpeed = new List<float>(pcarsDataStruct.TyreSlipSpeed);
            pCarsData.TyreTemp = new List<float>(pcarsDataStruct.TyreTemp);
            pCarsData.TyreGrip = new List<float>(pcarsDataStruct.TyreGrip);
            pCarsData.TyreHeightAboveGround = new List<float>(pcarsDataStruct.TyreHeightAboveGround);
            pCarsData.TyreLateralStiffness = new List<float>(pcarsDataStruct.TyreLateralStiffness);
            pCarsData.TyreWear = new List<float>(pcarsDataStruct.TyreWear);
            pCarsData.BrakeDamage = new List<float>(pcarsDataStruct.BrakeDamage);
            pCarsData.SuspensionDamage = new List<float>(pcarsDataStruct.SuspensionDamage);

            pCarsData.BrakeTempCelsius = new List<float>(pcarsDataStruct.BrakeTempCelsius);
            pCarsData.TyreTreadTemp = new List<float>(pcarsDataStruct.TyreTreadTemp);
            pCarsData.TyreLayerTemp = new List<float>(pcarsDataStruct.TyreLayerTemp);
            pCarsData.TyreCarcassTemp = new List<float>(pcarsDataStruct.TyreCarcassTemp);
            pCarsData.TyreRimTemp = new List<float>(pcarsDataStruct.TyreRimTemp);
            pCarsData.TyreInternalAirTemp = new List<float>(pcarsDataStruct.TyreInternalAirTemp);

            // Car Damage
            pCarsData.CrashState = (CrashDamageState)pcarsDataStruct.CrashState;
            pCarsData.AeroDamage = pcarsDataStruct.AeroDamage;
            pCarsData.EngineDamage = pcarsDataStruct.EngineDamage;

            // Weather
            pCarsData.AmbientTemperature = pcarsDataStruct.AmbientTemperature;
            pCarsData.TrackTemperature = pcarsDataStruct.TrackTemperature;
            pCarsData.RainDensity = pcarsDataStruct.RainDensity;
            pCarsData.WindSpeed = pcarsDataStruct.WindSpeed;
            pCarsData.WindDirectionX = pcarsDataStruct.WindDirectionX;
            pCarsData.WindDirectionY = pcarsDataStruct.WindDirectionY;
            pCarsData.CloudBrightness = pcarsDataStruct.CloudBrightness;

            return pCarsData;
        }
    }
}