﻿using FusionLibrary;
using FusionLibrary.Extensions;
using GTA;
using LemonUI.Menus;
using System;
using System.ComponentModel;
using static BackToTheFutureV.InternalEnums;

namespace BackToTheFutureV
{
    internal class TimeMachineMenu : BTTFVMenu
    {
        public NativeCheckboxItem TimeCircuitsOn { get; }
        public NativeCheckboxItem CutsceneMode { get; }
        public NativeCheckboxItem FlyMode { get; }
        public NativeCheckboxItem AltitudeHold { get; }
        public NativeSubmenuItem PhotoMenu { get; }

        public TimeMachineMenu() : base("TimeMachine")
        {
            TimeCircuitsOn = NewCheckboxItem("TC");
            CutsceneMode = NewCheckboxItem("Cutscene");
            FlyMode = NewCheckboxItem("Hover");
            AltitudeHold = NewCheckboxItem("Altitude");

            PhotoMenu = NewSubmenu(MenuHandler.PhotoMenu);
            NewSubmenu(MenuHandler.CustomMenu2);
            NewSubmenu(MenuHandler.MainMenu);
        }

        public override void Menu_OnItemActivated(NativeItem sender, EventArgs e)
        {

        }

        public override void Menu_Shown(object sender, EventArgs e)
        {
            if (CurrentTimeMachine == null || !FusionUtils.PlayerPed.IsFullyInVehicle())
            {
                Visible = false;
                return;
            }

            FlyMode.Enabled = CurrentTimeMachine.Mods.HoverUnderbody == ModState.On && !CurrentTimeMachine.Properties.AreFlyingCircuitsBroken;
            AltitudeHold.Enabled = FlyMode.Enabled;
            TimeCircuitsOn.Enabled = !Game.IsMissionActive;
        }

        public override void Menu_OnItemCheckboxChanged(NativeCheckboxItem sender, EventArgs e, bool Checked)
        {
            switch (sender)
            {
                case NativeCheckboxItem item when item == TimeCircuitsOn:
                    CurrentTimeMachine.Events.SetTimeCircuits?.Invoke(Checked);
                    break;
                case NativeCheckboxItem item when item == CutsceneMode:
                    CurrentTimeMachine.Events.SetCutsceneMode?.Invoke(Checked);
                    break;
                case NativeCheckboxItem item when item == FlyMode:
                    CurrentTimeMachine.Events.SetFlyMode?.Invoke(Checked);
                    break;
                case NativeCheckboxItem item when item == AltitudeHold:
                    CurrentTimeMachine.Events.SetAltitudeHold?.Invoke(Checked);
                    break;
            }
        }

        public override void Tick()
        {
            if (CurrentTimeMachine == null)
            {
                Visible = false;
                return;
            }

            TimeCircuitsOn.Checked = CurrentTimeMachine.Properties.AreTimeCircuitsOn;
            CutsceneMode.Checked = CurrentTimeMachine.Properties.CutsceneMode;
            FlyMode.Checked = CurrentTimeMachine.Properties.IsFlying;
            AltitudeHold.Checked = CurrentTimeMachine.Properties.IsAltitudeHolding;
            PhotoMenu.Enabled = !CurrentTimeMachine.Constants.FullDamaged && !Game.IsMissionActive;
        }

        public override void Menu_OnItemValueChanged(NativeSliderItem sender, EventArgs e)
        {

        }

        public override void Menu_OnItemSelected(NativeItem sender, SelectedEventArgs e)
        {

        }

        public override void Menu_Closing(object sender, CancelEventArgs e)
        {

        }
    }
}
