﻿using FusionLibrary;
using FusionLibrary.Extensions;
using GTA;
using LemonUI.Menus;
using System;
using System.ComponentModel;

namespace BackToTheFutureV
{
    internal class DoorsMenu : BTTFVMenu
    {
        private readonly NativeItem DriversDoor;
        private readonly NativeItem PassengerDoor;
        private readonly NativeItem Hood;
        private readonly NativeItem Trunk;
        private readonly NativeItem Engine;

        public DoorsMenu() : base("Doors")
        {
            DriversDoor = NewItem("DriversDoor");
            PassengerDoor = NewItem("PassengerDoor");
            Hood = NewItem("Hood");
            Trunk = NewItem("Trunk");
            Engine = NewItem("Engine");
        }

        public override void Tick()
        {
            DriversDoor.Enabled = FusionUtils.PlayerPed?.GetClosestVehicle(5f)?.Model == ModelHandler.DMC12 && !FusionUtils.PlayerPed.GetClosestVehicle(5f).IsConsideredDestroyed;
            PassengerDoor.Enabled = FusionUtils.PlayerPed?.GetClosestVehicle(5f)?.Model == ModelHandler.DMC12 && !FusionUtils.PlayerPed.GetClosestVehicle(5f).IsConsideredDestroyed;
            Hood.Enabled = FusionUtils.PlayerPed?.GetClosestVehicle(5f)?.Model == ModelHandler.DMC12 && !FusionUtils.PlayerPed.GetClosestVehicle(5f).IsConsideredDestroyed && !(FusionUtils.PlayerPed.GetClosestVehicle(5f).IsTimeMachine() && TimeMachineHandler.GetTimeMachineFromVehicle(FusionUtils.PlayerPed.GetClosestVehicle(5f)).Mods.Hoodbox == InternalEnums.ModState.On);
            Trunk.Enabled = FusionUtils.PlayerPed?.GetClosestVehicle(5f)?.Model == ModelHandler.DMC12 && !FusionUtils.PlayerPed.GetClosestVehicle(5f).IsConsideredDestroyed && !FusionUtils.PlayerPed.GetClosestVehicle(5f).IsTimeMachine();
            Engine.Enabled = FusionUtils.PlayerPed?.GetClosestVehicle(5f)?.Model == ModelHandler.DMC12 && !FusionUtils.PlayerPed.GetClosestVehicle(5f).IsConsideredDestroyed && !FusionUtils.PlayerPed.GetClosestVehicle(5f).IsTimeMachine();
        }

        public override void Menu_OnItemActivated(NativeItem sender, EventArgs e)
        {
            Vehicle vehicle = FusionUtils.PlayerPed.GetClosestVehicle(5f);

            switch (sender)
            {
                case NativeItem item when item == DriversDoor:
                    if (vehicle.Doors[VehicleDoorIndex.FrontLeftDoor].IsOpen)
                    {
                        vehicle.Doors[VehicleDoorIndex.FrontLeftDoor].Close();
                    }
                    else
                    {
                        vehicle.Doors[VehicleDoorIndex.FrontLeftDoor].Open();
                    }
                    break;
                case NativeItem item when item == PassengerDoor:
                    if (vehicle.Doors[VehicleDoorIndex.FrontRightDoor].IsOpen)
                    {
                        vehicle.Doors[VehicleDoorIndex.FrontRightDoor].Close();
                    }
                    else
                    {
                        vehicle.Doors[VehicleDoorIndex.FrontRightDoor].Open();
                    }
                    break;
                case NativeItem item when item == Hood:
                    if (vehicle.Doors[VehicleDoorIndex.Hood].IsOpen)
                    {
                        vehicle.Doors[VehicleDoorIndex.Hood].Close();
                    }
                    else
                    {
                        vehicle.Doors[VehicleDoorIndex.Hood].Open();
                    }
                    break;
                case NativeItem item when item == Trunk:
                    if (vehicle.Doors[VehicleDoorIndex.Trunk].IsOpen && !vehicle.Doors[VehicleDoorIndex.BackRightDoor].IsOpen)
                    {
                        vehicle.Doors[VehicleDoorIndex.Trunk].Close();
                    }
                    else if (vehicle.Doors[VehicleDoorIndex.BackRightDoor].IsOpen)
                    {
                        vehicle.Doors[VehicleDoorIndex.BackRightDoor].Close();
                        vehicle.Doors[VehicleDoorIndex.Trunk].Close();
                    }
                    else
                    {
                        vehicle.Doors[VehicleDoorIndex.Trunk].Open();
                    }
                    break;
                case NativeItem item when item == Engine:
                    if (vehicle.Doors[VehicleDoorIndex.BackRightDoor].IsOpen)
                    {
                        vehicle.Doors[VehicleDoorIndex.BackRightDoor].Close();
                    }
                    else if (vehicle.Doors[VehicleDoorIndex.Trunk].IsOpen && !vehicle.Doors[VehicleDoorIndex.BackRightDoor].IsOpen)
                    {
                        vehicle.Doors[VehicleDoorIndex.BackRightDoor].Open();
                    }
                    else if (!vehicle.Doors[VehicleDoorIndex.Trunk].IsOpen && !vehicle.Doors[VehicleDoorIndex.BackRightDoor].IsOpen)
                    {
                        vehicle.Doors[VehicleDoorIndex.Trunk].Open();
                        vehicle.Doors[VehicleDoorIndex.BackRightDoor].Open();
                    }
                    break;
            }
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
