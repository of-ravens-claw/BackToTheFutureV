using FusionLibrary;
using GTA;
using GTA.Native;
using KlangRageAudioLibrary;
using System;
using System.Windows.Forms;
using Screen = GTA.UI.Screen;
using static BackToTheFutureV.Logger;

namespace BackToTheFutureV
{
    internal class Main : Script
    {
        public static Version Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public static AudioEngine CommonAudioEngine { get; set; } = new AudioEngine() { BaseSoundFolder = "BackToTheFutureV\\Sounds" };
        public static bool FirstTick { get; private set; } = true;
        public static CustomStopwatch CustomStopwatch { get; } = new CustomStopwatch();
        public static DateTime NewGameTime { get; } = new DateTime(2003, 12, 15, 5, 0, 0);
        public static bool FirstMission { get; private set; }

        public Main()
        {
            LogCheck();
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(Version.Build).AddSeconds(Version.Revision * 2).ToUniversalTime();

            LogI($"Initialisation was successful! - Built on: {buildDate} (UTC), Version №: v{Version}.");
            LogI("This is an *unofficial* build. Do not ask for support whilst using it. It is intentionally missing features, hence 'stripped'.");
            //InitSystem();
            ModSettings.LoadSettings();

            Tick += Main_Tick;
            KeyDown += Main_KeyDown;
            Aborted += Main_Aborted;
        }

        private void Main_Aborted(object sender, EventArgs e)
        {
            World.RenderingCamera = null;

            Screen.FadeIn(1000);
            GarageHandler.Abort();
            MissionHandler.Abort();
            StoryTimeMachineHandler.Abort();
            TimeMachineHandler.Abort();
            FireTrailsHandler.Abort();
            CustomTrainHandler.Abort();
            DMC12Handler.Abort();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            TimeMachineHandler.KeyDown(e);
            MissionHandler.KeyDown(e);
            MenuHandler.KeyDown(e);
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            if (Game.IsLoading || FusionUtils.FirstTick) return;
            if (FirstTick && FusionUtils.CurrentTime == NewGameTime && Game.IsMissionActive) FirstMission = true;
            if (FirstMission && Game.IsMissionActive) return;
            if (FirstMission) FirstMission = false;
            
            if (FirstTick)
            {
                ModelHandler.RequestModels();

                // Disable fake shake of the cars.
                Function.Call(Hash.SET_CAR_HIGH_SPEED_BUMP_SEVERITY_MULTIPLIER, 0.0f);

                FusionUtils.RandomTrains = ModSettings.RandomTrains;

                DecoratorsHandler.Register();
                WeatherHandler.Register();
            }

            CustomTrainHandler.Tick();
            DMC12Handler.Tick();
            TimeMachineHandler.Tick();
            FireTrailsHandler.Tick();
            TcdEditer.Tick();
            RCGUIEditer.Tick();
            MissionHandler.Tick();
            StoryTimeMachineHandler.Tick();
            MenuHandler.Tick();
            TrashHandler.Tick();
            GarageHandler.Tick();
            WeatherHandler.Tick();
            if (FirstTick) FirstTick = false;
        }
    }
}
