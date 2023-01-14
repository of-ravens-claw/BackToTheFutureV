using FusionLibrary;
using LemonUI.Menus;
using System;
using System.ComponentModel;

namespace BackToTheFutureV
{
    internal class SettingsMenu : BTTFVMenu
    {
        private readonly NativeCheckboxItem cinematicSpawn;
        private readonly NativeCheckboxItem useInputToggle;
        private readonly NativeCheckboxItem LandingSystem;
        private readonly NativeCheckboxItem InfiniteFuel;
        private readonly NativeCheckboxItem RandomTrains;
        private readonly NativeCheckboxItem GlowingWormholeEmitter;
        private readonly NativeCheckboxItem GlowingPlutoniumReactor;

        public SettingsMenu() : base("Settings")
        {
            cinematicSpawn = NewCheckboxItem("CinematicSpawn", ModSettings.CinematicSpawn);
            useInputToggle = NewCheckboxItem("InputToggle", ModSettings.UseInputToggle);
            LandingSystem = NewCheckboxItem("LandingSystem", ModSettings.LandingSystem);
            InfiniteFuel = NewCheckboxItem("InfinityReactor", ModSettings.InfiniteFuel);
            RandomTrains = NewCheckboxItem("RandomTrains", ModSettings.RandomTrains);
            GlowingWormholeEmitter = NewCheckboxItem("GlowingWormhole", ModSettings.GlowingWormholeEmitter);
            GlowingPlutoniumReactor = NewCheckboxItem("GlowingReactor", ModSettings.GlowingPlutoniumReactor);

            NewSubmenu(MenuHandler.SoundsSettingsMenu);

            NewSubmenu(MenuHandler.EventsSettingsMenu);

            NewSubmenu(MenuHandler.ControlsMenu);

            NewSubmenu(MenuHandler.TCDMenu);
        }

        public override void Menu_Shown(object sender, EventArgs e)
        {
            if (FusionUtils.RandomTrains != ModSettings.RandomTrains)
            {
                ModSettings.RandomTrains = FusionUtils.RandomTrains;
                ModSettings.SaveSettings();

                RandomTrains.Checked = FusionUtils.RandomTrains;
            }
        }

        public override void Menu_OnItemCheckboxChanged(NativeCheckboxItem sender, EventArgs e, bool Checked)
        {
            switch (sender)
            {
                case NativeCheckboxItem _ when sender == cinematicSpawn:
                    ModSettings.CinematicSpawn = Checked;
                    break;
                case NativeCheckboxItem _ when sender == useInputToggle:
                    ModSettings.UseInputToggle = Checked;
                    break;
                case NativeCheckboxItem _ when sender == LandingSystem:
                    ModSettings.LandingSystem = Checked;
                    break;
                case NativeCheckboxItem _ when sender == RandomTrains:
                    ModSettings.RandomTrains = Checked;
                    FusionUtils.RandomTrains = Checked;
                    break;
                case NativeCheckboxItem _ when sender == GlowingWormholeEmitter:
                    ModSettings.GlowingWormholeEmitter = Checked;
                    break;
                case NativeCheckboxItem _ when sender == GlowingPlutoniumReactor:
                    ModSettings.GlowingPlutoniumReactor = Checked;
                    break;
                case NativeCheckboxItem _ when sender == InfiniteFuel:
                    ModSettings.InfiniteFuel = Checked;
                    break;
            }

            ModSettings.SaveSettings();
        }

        public override void Tick()
        {
        }

        public override void Menu_OnItemValueChanged(NativeSliderItem sender, EventArgs e)
        {
        }

        public override void Menu_OnItemSelected(NativeItem sender, SelectedEventArgs e)
        {
        }

        public override void Menu_OnItemActivated(NativeItem sender, EventArgs e)
        {
        }

        public override void Menu_Closing(object sender, CancelEventArgs e)
        {
        }
    }
}
