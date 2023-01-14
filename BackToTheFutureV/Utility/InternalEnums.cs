using System;

namespace BackToTheFutureV
{
    internal static class InternalEnums
    {
        internal enum EmptyType
        {
            Hide,
            Off,
            On
        }

        internal static class BTTFVDecors
        {
            public const string TimeMachine = "BTTFV_TimeMachine";
            public const string DestDate1 = "BTTFV_DestDate1";
            public const string DestDate2 = "BTTFV_DestDate2";
            public const string LastDate1 = "BTTFV_LastDate1";
            public const string LastDate2 = "BTTFV_LastDate2";
            public const string WormholeType = "BTTFV_WormholeType";
            public const string TimeCircuitsOn = "BTTFV_TCOn";
            public const string CutsceneMode = "BTTFV_TimeTravelType";
            public const string TorqueMultiplier = "BTTFV_TorqueMultiplier";
        }
        

        internal enum GarageStatus
        {
            Idle,
            Busy,
            Opening
        }

        internal enum SparkType
        {
            WHE,
            Left,
            Right
        }

        internal enum ReactorState
        {
            Closed,
            Opened,
            Refueling
        }

        internal enum TCDBackground
        {
            BTTF1, BTTF3, Transparent
        }

        internal enum TimeTravelPhase
        {
            Completed = 0,
            OpeningWormhole = 1,
            InTime = 2,
            Reentering = 3
        }

        internal enum ReenterType
        {
            Normal,
            Spawn
        }

        internal enum TimeTravelType
        {
            Cutscene,
            Instant
        }

        internal enum MissionType
        {
            None,
            Escape,
            Train
        }

        internal enum TimeMachineCamera
        {
            Default = -1,
            DigitalSpeedo,
            PlateCustom,
            ReactorCustom,
            RimCustom,
            HoodCustom,
            ExhaustCustom,
            HookCustom,
            SuspensionsCustom,
            HoverUnderbodyCustom,
            BulovaSetup
        }

        internal enum WormholeType
        {
            Unknown = -1,
            DMC12,
            BTTF1,
            BTTF2,
            BTTF3
        }

        internal enum ModState
        {
            Off = -1,
            On = 0
        }

        internal enum HookState
        {
            Off,
            OnDoor,
            On,
            Removed,
            Unknown
        }

        internal enum PlateType
        {
            Empty = -1,
            Outatime = 0,
            Futuristic = 1,
            Notime = 2,
            Timeless = 3,
            Timeless2 = 4,
            Dmcfactory = 5,
            Dmcfactory2 = 6
        }

        internal enum ReactorType
        {
            None = -1,
            MrFusion = 0,
            Nuclear = 1
        }

        internal enum ExhaustType
        {
            Stock = -1,
            BTTF = 0,
            None = 1
        }

        internal enum WheelType
        {
            Stock = -1,
            StockInvisible = 210,
            RailroadInvisible = 211,
            RedInvisible = 212,
            Red = 213,
            [Obsolete]
            Dmc = 214,
            DmcInvisible = 215
        }

        internal enum SuspensionsType
        {
            Unknown = -1,
            Stock = 0,
            LiftFrontLowerRear = 1,
            LiftFront = 2,
            LiftRear = 3,
            LiftFrontAndRear = 4,
            LowerFrontLiftRear = 5,
            LowerFront = 6,
            LowerRear = 7,
            LowerFrontAndRear = 8
        }

        internal enum HoodType
        {
            Stock = -1,
            H1983 = 0,
            H1981 = 1
        }
    }
}
