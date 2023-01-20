using System;
using System.ComponentModel;
using GTA;
using GTA.Native;
using GTA.UI;
using GTA.Math;
using FusionLibrary;
using FusionLibrary.Extensions;
using LemonUI.Menus;
using System.Windows.Forms;
using static FusionLibrary.FusionEnums;

namespace BackToTheFutureV
{
    internal class DebugMenu : BTTFVMenu
    {
        private readonly NativeItem test;
        private readonly NativeCheckboxItem debugFuel;
        private readonly NativeCheckboxItem debugWarmBox;
        private readonly NativeCheckboxItem debugDamageStuff; // Weird name. I know.
        private readonly NativeCheckboxItem forceFlyMode;
        private readonly NativeCheckboxItem PersistenceSystem;
        private readonly NativeSubmenuItem CustomMenuMain;
        public DebugMenu() : base("Debug")
        {
            Add(test = new NativeItem("Test", "This is purely a test"));
            
            Add(debugFuel = new NativeCheckboxItem(Game.GetLocalizedString("BTTFV_Menu_Settings_Item_InfinityReactor_Title"), "When enabled, player doesn't have to refuel the time machine.", DebugSettings.DebugFuel));
            
            Add(debugWarmBox = new NativeCheckboxItem("Always Warm Hoodbox", "When enabled, player doesn't have to leave the car to warm the hoodbox up.", DebugSettings.DebugWarmBox));
            
            Add(debugDamageStuff = new NativeCheckboxItem("Disable Damage", "I have no idea how to explain this properly. But, it disables the 1885 glitch, and the flight controls/time circuits from being damaged.\n~y~WARNING~s~: At the moment, this does ~r~nothing!~s~", DebugSettings.DebugDamageStuff));
            
            Add(forceFlyMode = new NativeCheckboxItem(Game.GetLocalizedString("BTTFV_Menu_Settings_Item_ForceFly_Title"), Game.GetLocalizedString("BTTFV_Menu_Settings_Item_ForceFly_Description"), ModSettings.ForceFlyMode));
            
            Add(PersistenceSystem = new NativeCheckboxItem(Game.GetLocalizedString("BTTFV_Menu_Settings_Item_Persistence_Title"), Game.GetLocalizedString("BTTFV_Menu_Settings_Item_Persistence_Description"), ModSettings.PersistenceSystem));

            CustomMenuMain = NewSubmenu(MenuHandler.CustomMenuMain);
        }
        public override void Tick()
        {
            CustomMenuMain.Enabled = !CurrentTimeMachine.Constants.FullDamaged && !CurrentTimeMachine.Properties.IsRemoteControlled;
            PersistenceSystem.Enabled = !ModSettings.WaybackSystem;
        }

        public override void Menu_OnItemActivated(NativeItem sender, EventArgs e)
        {
            switch (sender)
            {
                case NativeItem item when item == test:
                    //GTA.UI.Screen.ShowSubtitle($"");
                    CurrentTimeMachine.Events.SimulateInputDate?.Invoke(FusionUtils.CurrentTime);
                    //CurrentTimeMachine.Events.SimulateHoverGoingUpDown?.Invoke(true);
                    //CurrentTimeMachine.Events.SimulateHoverBoost?.Invoke(true);
                    //CurrentTimeMachine.Events.SetTimeCircuits?.Invoke(true);
                    //Script.Wait(4000);
                    //CurrentTimeMachine.Events.StartTimeTravel?.Invoke();
                    //CurrentTimeMachine.Events.SetHoodboxWarmedUp?.Invoke();
                    break;
            }
        }

        public override void Menu_OnItemCheckboxChanged(NativeCheckboxItem sender, EventArgs e, bool Checked)
        {
             switch (sender)
             {
                 case NativeCheckboxItem item when item == debugFuel:
                     DebugSettings.DebugFuel = Checked;
                     break;

                case NativeCheckboxItem item when item == debugWarmBox:
                    DebugSettings.DebugWarmBox = Checked;
                    break;
                 
                 case NativeCheckboxItem item when item == debugDamageStuff:
                    DebugSettings.DebugDamageStuff = Checked;
                    break;

                case NativeCheckboxItem _ when sender == PersistenceSystem:
                    ModSettings.PersistenceSystem = Checked;
                    if (!Checked)
                    {
                        TimeMachineCloneHandler.Delete();
                        RemoteTimeMachineHandler.DeleteAll();
                    }
                    break;
            }

            ModSettings.SaveSettings();
        }

        public override void Menu_OnItemSelected(NativeItem sender, SelectedEventArgs e)
        {
        }

        public override void Menu_Closing(object sender, CancelEventArgs e)
        {
        }

        public override void Menu_Shown(object sender, EventArgs e)
        {
            CustomMenuMain.Enabled = !CurrentTimeMachine.Properties.IsRemoteControlled; // I'll be honest, I don't even know if you can get to this menu via RC Mode, but it was there originally, so I ported it over.
        }

        public override void Menu_OnItemValueChanged(NativeSliderItem sender, EventArgs e)
        {
        }
    }
}