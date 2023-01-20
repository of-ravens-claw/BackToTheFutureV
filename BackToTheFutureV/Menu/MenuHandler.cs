using FusionLibrary;
using FusionLibrary.Extensions;
using GTA;
using System.Windows.Forms;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using static BackToTheFutureV.InternalEnums;
using Button = GTA.Button;

namespace BackToTheFutureV
{
    internal class MenuHandler
    {
        public static ControlsMenu ControlsMenu { get; } = new ControlsMenu();
        public static SoundsSettingsMenu SoundsSettingsMenu { get; } = new SoundsSettingsMenu();
        public static EventsSettingsMenu EventsSettingsMenu { get; } = new EventsSettingsMenu();
        public static TCDMenu TCDMenu { get; } = new TCDMenu();
        public static SettingsMenu SettingsMenu { get; } = new SettingsMenu();
        public static RCMenu RCMenu { get; } = new RCMenu();
        public static OverrideMenu OverrideMenu { get; } = new OverrideMenu();
        public static PhotoMenu PhotoMenu { get; } = new PhotoMenu();
        public static DoorsMenu DoorsMenu { get; } = new DoorsMenu();
        public static CustomMenu CustomMenuMain { get; } = new CustomMenu();
        public static CustomMenu CustomMenuPresets { get; } = new CustomMenu() { ForceNew = true };
        public static CustomMenu2 CustomMenuGarage { get; } = new CustomMenu2();
        public static GarageMenu GarageMenu { get; } = new GarageMenu();
        public static PresetsMenu PresetsMenu { get; } = new PresetsMenu();
        public static OutatimeMenu OutatimeMenu { get; } = new OutatimeMenu();
        public static DebugMenu DebugMenu { get; } = new DebugMenu();
        public static MainMenu MainMenu { get; } = new MainMenu();
        public static TimeMachineMenu TimeMachineMenu { get; } = new TimeMachineMenu();
<<<<<<< Updated upstream
        public static int closingTime;

=======
>>>>>>> Stashed changes
        public static bool UnlockPhotoMenu { get; private set; } = true;
        public static bool UnlockSpawnMenu { get; private set; } = true;
        public static bool UnlockDebugMenu { get; private set; } = true;

        public static bool IsAnyMenuOpen()
        {
            if (ControlsMenu.Visible || SoundsSettingsMenu.Visible || EventsSettingsMenu.Visible || TCDMenu.Visible || SettingsMenu.Visible || RCMenu.Visible || OverrideMenu.Visible || PhotoMenu.Visible || DoorsMenu.Visible || CustomMenuMain.Visible || CustomMenuPresets.Visible || CustomMenuGarage.Visible || GarageMenu.Visible || PresetsMenu.Visible || OutatimeMenu.Visible || MainMenu.Visible || TimeMachineMenu.Visible || DebugMenu.Visible)
            {
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// <b>ONLY FOR CONTROLLER!</b><br />
        /// Checks if a cheat has been activated. Uses special "formatting" for this.<br />
        /// <br />
        /// <b>ACTUAL NATIVE NAME:</b> HAS_CHEAT_WITH_HASH_BEEN_ACTIVATED<br />
        /// <b>PARAM NOTES:</b> Too big for the summary, check my gist instead.<br />
        /// https://gist.github.com/of-ravens-claw/43a212befce451dc28425eff9643125d
        /// <returns><see langword="true" /> if the cheat was just activated; otherwise, <see langword="false" /></returns>
        /// </summary>
        // May consider moving this to FusionLibrary.
        private static bool HasControllerCheatBeenActivated(string cheatString)
        {
            // Calculating the length of the cheatString (we could let the user override it, but it's a bit more work, and makes it more prone to errors.)
            var lengthOfCheatString = Function.Call<int>(Hash.GET_LENGTH_OF_LITERAL_STRING, cheatString);
            // Technically it wants a hash, so we're converting that here with Game.GenerateHash()
            return Function.Call<bool>((Hash)0x071E2A839DE82D90, Game.GenerateHash(cheatString.ToLower()), lengthOfCheatString);
            // JOAAT Hashes are case-sensitive, we're sending ToLower, since Rockstar *mostly* uses them as purely lowercase.
        }
        
        public static void Tick()
        {
            // Actual name: HAS_PC_CHEAT_WITH_HASH_BEEN_ACTIVATED
            // Uses joaat hashes, unsigned. (no negative numbers)
            //if (Function.Call<bool>(Hash._HAS_CHEAT_STRING_JUST_BEEN_ENTERED, Game.GenerateHash("timeparadox"))) //timeparadox
            if (Game.WasCheatStringJustEntered("timeparadox") || HasControllerCheatBeenActivated("UUDDLR13"))
            {
                UnlockSpawnMenu = !UnlockSpawnMenu;
                Notification.Show(NotificationIcon.SocialClub, "BackToTheFutureV", "MenuHandler.cs",
                    $"Spawn Menu {string.Format(UnlockSpawnMenu.ToString(), "Unlocked", "Locked")}!", false,
                    false);
                if (!Function.Call<bool>(Hash._IS_USING_KEYBOARD, 0))
                    FusionUtils.SetPadShake(500, 200);
            }
            //if (Function.Call<bool>(Hash._HAS_CHEAT_STRING_JUST_BEEN_ENTERED, 2607119772)) //smiledoc
            if (Game.WasCheatStringJustEntered("smiledoc"))
            {
                UnlockPhotoMenu = !UnlockPhotoMenu;
                Notification.Show(NotificationIcon.SocialClub, "BackToTheFutureV", "MenuHandler.cs",
                    "Photo Menu unlocked!", false,
                    false);
            }

            //if (Function.Call<bool>(Hash._HAS_CHEAT_STRING_JUST_BEEN_ENTERED, 423637724)) //ranstar, may change it in the future.
            if (Game.WasCheatStringJustEntered("ranstar74"))
            {
                UnlockDebugMenu = !UnlockDebugMenu;
                Notification.Show(NotificationIcon.SocialClub, "BackToTheFutureV", "MenuHandler.cs",
                    "Debug Menu unlocked!", false,
                    false);
            }
            
            //if (Function.Call<bool>(Hash._HAS_CHEAT_STRING_JUST_BEEN_ENTERED, 3498824929)) //bttfv
            if (Game.WasCheatStringJustEntered("bttfv"))
                MainMenu.Visible = true;

            if (!TcdEditer.IsEditing && !RCGUIEditer.IsEditing && GarageHandler.Status == GarageStatus.Idle)
            {
                if ((ModControls.CombinationsForInteractionMenu && Game.IsEnabledControlPressed(ModControls.InteractionMenu1) && Game.IsControlPressed(ModControls.InteractionMenu2)) || (!ModControls.CombinationsForInteractionMenu && Game.IsControlPressed(ModControls.InteractionMenu1)))
                {
                    if (TimeMachineHandler.CurrentTimeMachine != null)
                    {
                        if (TimeMachineHandler.CurrentTimeMachine.Properties.TimeTravelPhase > TimeTravelPhase.OpeningWormhole) return;
                    }

                    if (FusionUtils.PlayerPed.IsGoingIntoCover)
                    {
                        FusionUtils.PlayerPed.Task.StandStill(1);
                    }

                    if (RemoteTimeMachineHandler.IsRemoteOn)
                    {
                        TimeMachineMenu.Visible = true;
                        return;
                    }

                    if (CustomNativeMenu.ObjectPool.AreAnyVisible) return;


                    if (TimeMachineHandler.CurrentTimeMachine != null)
                    {
                        TimeMachineMenu.Visible = true;
                    }
                    else
                    {
                        MainMenu.Visible = true;
                    }
                }

                if ((MainMenu.Visible || TimeMachineMenu.Visible || GarageMenu.Visible || CustomMenuMain.Visible || CustomMenuPresets.Visible || PresetsMenu.Visible || PhotoMenu.Visible) && FusionUtils.PlayerVehicle.NotNullAndExists() && (Game.IsControlJustPressed(GTA.Control.VehicleCinCam) || Game.IsControlJustPressed(GTA.Control.VehicleDuck)))
                {
                    closingTime = Game.GameTime + 256;
                }
            }

            if (Game.GameTime < closingTime)
            {
                Game.DisableControlThisFrame(GTA.Control.VehicleCinCam);
            }
        }

        public static void KeyDown(KeyEventArgs e)
        {
            if (TcdEditer.IsEditing || RCGUIEditer.IsEditing || GarageHandler.Status != GarageStatus.Idle) return;

            if ((ModControls.UseControlForMainMenu && e.Control && e.KeyCode == ModControls.MainMenu) || (!ModControls.UseControlForMainMenu && !e.Control && e.KeyCode == ModControls.MainMenu))
            {
                if (TimeMachineHandler.CurrentTimeMachine != null)
                {
                    if (TimeMachineHandler.CurrentTimeMachine.Properties.TimeTravelPhase > TimeTravelPhase.OpeningWormhole) return;
                }

                CustomNativeMenu.ObjectPool.HideAll();

                MainMenu.Visible = true;
            }

            /*if (e.Alt && e.KeyCode == Keys.D1)
            {
                string hash = Game.GetUserInput(WindowTitle.EnterMessage20, "", 20).ToLower().GetSHA256Hash();

                switch (hash)
                {
                    case "c3cca7029c38959a99b7aa57c37f0b05b663fd624a8f7dbc6424e44320b84206":
                        UnlockSpawnMenu = !UnlockSpawnMenu;
                        break;
                    case "fbff03e5367d548c10cb18965f950df472a8dc408d003f557ce974ddc2658ade":
                        UnlockPhotoMenu = !UnlockPhotoMenu;
                        break;
                    case "db8d4feddd02cd8b9f5e512bb933f044bcbd154ccb210493348dd8a2b25bcd56":
                        UnlockDebugMenu = !UnlockDebugMenu;
                        break;
                    case "5dd6475a70795493dd0b5a53b7c488dd9924a833ccd555901e353841c307f03b":
                        MainMenu.Visible = true;
                        break;
                }
            }*/
        }
    }
}
