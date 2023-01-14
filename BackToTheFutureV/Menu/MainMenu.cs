using BackToTheFutureV;
using FusionLibrary;
using FusionLibrary.Extensions;
using GTA;
using LemonUI.Menus;
using System;
using System.ComponentModel;
using static BackToTheFutureV.InternalEnums;
using static FusionLibrary.FusionEnums;

namespace BackToTheFutureV
{
    internal class MainMenu : BTTFVMenu
    {
        private readonly NativeListItem<string> spawnBTTF;

        private readonly NativeItem deleteCurrent;
        private readonly NativeItem deleteOthers;
        private readonly NativeItem deleteAll;

        public MainMenu() : base("Main")
        {
            spawnBTTF = NewListItem("Spawn", TextHandler.Me.GetLocalizedText("DMC12", "BTTF1", "BTTF1H", "BTTF2", "BTTF3", "BTTF3RR"));
            spawnBTTF.ItemChanged += SpawnBTTF_ItemChanged;
            spawnBTTF.Description = GetItemValueDescription("Spawn", "DMC12");

            deleteCurrent = NewItem("Remove");
            deleteOthers = NewItem("RemoveOther");
            deleteAll = NewItem("RemoveAll");
            NewSubmenu(MenuHandler.SettingsMenu);
        }

        private void SpawnBTTF_ItemChanged(object sender, ItemChangedEventArgs<string> e)
        {
            switch (e.Index)
            {
                case 0:
                    spawnBTTF.Description = GetItemValueDescription(sender, "DMC12");
                    break;
                case 1:
                case 2:
                    spawnBTTF.Description = GetItemValueDescription(sender, "BTTF1");
                    break;
                case 3:
                    spawnBTTF.Description = GetItemValueDescription(sender, "BTTF2");
                    break;
                case 4:
                    spawnBTTF.Description = GetItemValueDescription(sender, "BTTF3");
                    break;
                case 5:
                    spawnBTTF.Description = GetItemValueDescription(sender, "BTTF3RR");
                    break;
            }

            Recalculate();
        }

        public override void Tick()
        {
        }

        public override void Menu_OnItemActivated(NativeItem sender, EventArgs e)
        {
            TimeMachine timeMachine;

            if (sender == spawnBTTF)
            {
                if (spawnBTTF.SelectedIndex == 0)
                {
                    FusionUtils.PlayerPed.Task.WarpIntoVehicle(DMC12Handler.CreateDMC12(FusionUtils.PlayerPed.Position, FusionUtils.PlayerPed.Heading), VehicleSeat.Driver);
                    Visible = false;
                    return;
                }

                WormholeType wormholeType = WormholeType.BTTF1;

                switch (spawnBTTF.SelectedIndex)
                {
                    case 3:
                        wormholeType = WormholeType.BTTF2;
                        break;
                    case 4:
                    case 5:
                        wormholeType = WormholeType.BTTF3;
                        break;
                }

                if (ModSettings.CinematicSpawn)
                {
                    timeMachine = TimeMachineHandler.Create(SpawnFlags.ForceReentry | SpawnFlags.New, wormholeType);
                }
                else
                {
                    timeMachine = TimeMachineHandler.Create(SpawnFlags.WarpPlayer | SpawnFlags.New, wormholeType);
                }

                if (spawnBTTF.SelectedIndex == 2)
                {
                    timeMachine.Mods.Hook = HookState.OnDoor;
                    timeMachine.Mods.Plate = PlateType.Empty;
                    timeMachine.Mods.Bulova = ModState.On;

                    timeMachine.Properties.ReactorCharge = 0;
                }

                if (spawnBTTF.SelectedIndex == 5)
                {
                    timeMachine.Mods.Wheel = WheelType.Railroad;
                }
            }

            if (sender == deleteCurrent)
            {
                timeMachine = TimeMachineHandler.GetTimeMachineFromVehicle(FusionUtils.PlayerVehicle);

                if (timeMachine == null && FusionUtils.PlayerVehicle != null && FusionUtils.PlayerVehicle.Model == ModelHandler.DMC12)
                {
                    FusionUtils.PlayerVehicle.Delete();
                    return;
                }
                
                if (timeMachine == null)
                {
                    TextHandler.Me.ShowNotification("NotSeated");
                    return;
                }

                TimeMachineHandler.RemoveTimeMachine(timeMachine);
            }

            if (sender == deleteOthers)
            {
                TimeMachineHandler.RemoveAllTimeMachines(true);
                TextHandler.Me.ShowNotification("RemovedOtherTimeMachines");
            }

            if (sender == deleteAll)
            {
                TimeMachineHandler.RemoveAllTimeMachines();
                TextHandler.Me.ShowNotification("RemovedAllTimeMachines");
            }

            Visible = false;
        }

        public override void Menu_OnItemCheckboxChanged(NativeCheckboxItem sender, EventArgs e, bool Checked)
        {

        }

        public override void Menu_OnItemSelected(NativeItem sender, SelectedEventArgs e)
        {

        }

        public override void Menu_Closing(object sender, CancelEventArgs e)
        {

        }

        public override void Menu_Shown(object sender, EventArgs e)
        {

        }

        public override void Menu_OnItemValueChanged(NativeSliderItem sender, EventArgs e)
        {

        }
    }
}
