using GTA;

namespace BackToTheFutureV
{
    internal class DebugSettings
    {
        public static bool DebugFuel { get; set; } = false;
        public static bool DebugWarmBox { get; set; } = false;
        public static bool DebugDamageStuff { get; set; } = false;
        public static void LoadDebug(ScriptSettings settings)
        {
            DebugFuel = settings.GetValue("Debug", "DebugFuel", DebugFuel);
            DebugWarmBox = settings.GetValue("Debug", "DebugWarmBox", DebugWarmBox);
            DebugDamageStuff = settings.GetValue("Debug", "DebugDamageStuff", DebugDamageStuff);

            SaveDebug(settings);
        }

        public static void SaveDebug(ScriptSettings settings)
        {
            settings.SetValue("Debug", "DebugFuel", DebugFuel);
            settings.SetValue("Debug", "DebugWarmBox", DebugWarmBox);
            settings.SetValue("Debug", "DebugDamageStuff", DebugDamageStuff);

            settings.Save();
        }
    }
}
