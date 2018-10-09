using System.Runtime.InteropServices;

namespace pCarsAPI_Demo
{
    public struct pCarsAPIParticipantStruct
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool IsActive;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)ApiStructLengths.StringLengthMax)]
        public string Name;
        // [ string ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Vector.VecMax)]
        public float[] WorldPosition;
        // [ UNITS = World Space  X  Y  Z ]

        public float CurrentLapDistance; // [ UNITS = Metres ]   [ RANGE = 0.0f->... ]    [ UNSET = 0.0f ]
        public uint RacePosition; // [ RANGE = 1->... ]   [ UNSET = 0 ]
        public uint LapsCompleted; // [ RANGE = 0->... ]   [ UNSET = 0 ]
        public uint CurrentLap; // [ RANGE = 0->... ]   [ UNSET = 0 ]
        public uint CurrentSector; // [ enum (Type#4) Current Sector ]
    }

    public struct pCarsAPIStruct
    {
        //SMS supplied data structure
        // Version Number
        public uint Version; // [ RANGE = 0->... ]
        public uint BuildVersion; // [ RANGE = 0->... ]   [ UNSET = 0 ]

        // Session type
        public uint GameState; // [ enum (Type#1) Game state ]
        public uint SessionState; // [ enum (Type#2) Session state ]
        public uint RaceState; // [ enum (Type#3) Race State ]

        // Participant Info
        public int ViewedParticipantIndex; // [ RANGE = 0->STORED_PARTICIPANTS_MAX ]   [ UNSET = -1 ]
        public int NumParticipants; // [ RANGE = 0->STORED_PARTICIPANTS_MAX ]   [ UNSET = -1 ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)ApiStructLengths.NumParticipants)]
        public
            pCarsAPIParticipantStruct[] ParticipantData;

        // Unfiltered Input
        public float UnfilteredThrottle; // [ RANGE = 0.0f->1.0f ]
        public float UnfilteredBrake; // [ RANGE = 0.0f->1.0f ]
        public float UnfilteredSteering; // [ RANGE = -1.0f->1.0f ]
        public float UnfilteredClutch; // [ RANGE = 0.0f->1.0f ]

        // Vehicle & Track information
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)ApiStructLengths.StringLengthMax)]
        public string
            CarName; // [ string ]

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)ApiStructLengths.StringLengthMax)]
        public string
            CarClassName; // [ string ]

        public uint LapsInEvent; // [ RANGE = 0->... ]   [ UNSET = 0 ]

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)ApiStructLengths.StringLengthMax)]
        public string
            TrackLocation; // [ string ]

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)ApiStructLengths.StringLengthMax)]
        public string
            TrackVariation; // [ string ]

        public float TrackLength; // [ UNITS = Metres ]   [ RANGE = 0.0f->... ]    [ UNSET = 0.0f ]

        // Timing & Scoring
        public bool LapInvalidated; // [ UNITS = boolean ]   [ RANGE = false->true ]   [ UNSET = false ]
        public float SessionFastestLapTime; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float LastLapTime; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        public float CurrentTime; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        public float SplitTimeAhead; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float SplitTimeBehind; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float SplitTime; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        public float EventTimeRemaining; // [ UNITS = milli-seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float PersonalFastestLapTime; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float WorldFastestLapTime; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float CurrentSector1Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float CurrentSector2Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float CurrentSector3Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float SessionFastestSector1Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float SessionFastestSector2Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float SessionFastestSector3Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float PersonalFastestSector1Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float PersonalFastestSector2Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float PersonalFastestSector3Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float WorldFastestSector1Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float WorldFastestSector2Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public float WorldFastestSector3Time; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]

        // Flags
        public uint HighestFlagColour; // [ enum (Type#5) Flag Colour ]
        public uint HighestFlagReason; // [ enum (Type#6) Flag Reason ]

        // Pit Info
        public uint PitMode; // [ enum (Type#7) Pit Mode ]
        public uint PitSchedule; // [ enum (Type#8) Pit Stop Schedule ]

        // Car State
        public uint CarFlags; // [ enum (Type#6) Car Flags ]
        public float OilTempCelsius; // [ UNITS = Celsius ]   [ UNSET = 0.0f ]
        public float OilPressureKPa; // [ UNITS = Kilopascal ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        public float WaterTempCelsius; // [ UNITS = Celsius ]   [ UNSET = 0.0f ]
        public float WaterPressureKPa; // [ UNITS = Kilopascal ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        public float FuelPressureKPa; // [ UNITS = Kilopascal ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        public float FuelLevel; // [ RANGE = 0.0f->1.0f ]
        public float FuelCapacity; // [ UNITS = Liters ]   [ RANGE = 0.0f->1.0f ]   [ UNSET = 0.0f ]
        public float Speed; // [ UNITS = Metres per-second ]   [ RANGE = 0.0f->... ]
        public float RPM; // [ UNITS = Revolutions per minute ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        public float MaxRPM; // [ UNITS = Revolutions per minute ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        public float Brake; // [ RANGE = 0.0f->1.0f ]
        public float Throttle; // [ RANGE = 0.0f->1.0f ]
        public float Clutch; // [ RANGE = 0.0f->1.0f ]
        public float Steering; // [ RANGE = -1.0f->1.0f ]

        public int Gear;
        // [ RANGE = -1 (Reverse)  0 (Neutral)  1 (Gear 1)  2 (Gear 2)  etc... ]   [ UNSET = 0 (Neutral) ]

        public int NumGears; // [ RANGE = 0->... ]   [ UNSET = -1 ]
        public float OdometerKM; // [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        public bool AntiLockActive; // [ UNITS = boolean ]   [ RANGE = false->true ]   [ UNSET = false ]
        public int LastOpponentCollisionIndex; // [ RANGE = 0->STORED_PARTICIPANTS_MAX ]   [ UNSET = -1 ]
        public float LastOpponentCollisionMagnitude; // [ RANGE = 0.0f->... ]
        public bool BoostActive; // [ UNITS = boolean ]   [ RANGE = false->true ]   [ UNSET = false ]
        public float BoostAmount; // [ RANGE = 0.0f->100.0f ] 

        // Motion & Device Related
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Vector.VecMax)]
        public float[] Orientation;
        // [ UNITS = Euler Angles ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Vector.VecMax)]
        public float[] LocalVelocity;
        // [ UNITS = Metres per-second ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Vector.VecMax)]
        public float[] WorldVelocity;
        // [ UNITS = Metres per-second ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Vector.VecMax)]
        public float[] AngularVelocity;
        // [ UNITS = Radians per-second ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Vector.VecMax)]
        public float[] LocalAcceleration;
        // [ UNITS = Metres per-second ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Vector.VecMax)]
        public float[] WorldAcceleration;
        // [ UNITS = Metres per-second ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Vector.VecMax)]
        public float[] ExtentsCentre;
        // [ UNITS = Local Space  X  Y  Z ]

        // Wheels / Tyres
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public uint[] TyreFlags;
        // [ enum (Type#7) Tyre Flags ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public uint[] Terrain;
        // [ enum (Type#3) Terrain Materials ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreY;
        // [ UNITS = Local Space  Y ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreRPS;
        // [ UNITS = Revolutions per second ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreSlipSpeed;
        // [ UNITS = Metres per-second ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreTemp;
        // [ UNITS = Celsius ]   [ UNSET = 0.0f ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreGrip;
        // [ RANGE = 0.0f->1.0f ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreHeightAboveGround;
        // [ UNITS = Local Space  Y ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreLateralStiffness;
        // [ UNITS = Lateral stiffness coefficient used in tyre deformation ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreWear;
        // [ RANGE = 0.0f->1.0f ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] BrakeDamage;
        // [ RANGE = 0.0f->1.0f ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] SuspensionDamage;
        // [ RANGE = 0.0f->1.0f ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] BrakeTempCelsius;
        // [ RANGE = 0.0f->1.0f ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreTreadTemp;
        // [ UNITS = Kelvin ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreLayerTemp;
        // [ UNITS = Kelvin ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreCarcassTemp;
        // [ UNITS = Kelvin ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreRimTemp;
        // [ UNITS = Kelvin ]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Tyres.TyreMax)]
        public float[] TyreInternalAirTemp;
        // [ UNITS = Kelvin ]

        // Car Damage
        public uint CrashState; // [ enum (Type#4) Crash Damage State ]
        public float AeroDamage; // [ RANGE = 0.0f->1.0f ]
        public float EngineDamage; // [ RANGE = 0.0f->1.0f ]

        // Weather
        public float AmbientTemperature; // [ UNITS = Celsius ]   [ UNSET = 25.0f ]
        public float TrackTemperature; // [ UNITS = Celsius ]   [ UNSET = 30.0f ]
        public float RainDensity; // [ UNITS = How much rain will fall ]   [ RANGE = 0.0f->1.0f ]
        public float WindSpeed; // [ RANGE = 0.0f->100.0f ]   [ UNSET = 2.0f ]
        public float WindDirectionX; // [ UNITS = Normalised Vector X ]
        public float WindDirectionY; // [ UNITS = Normalised Vector Y ]
        public float CloudBrightness; // [ RANGE = 0.0f->... ]
    }
}