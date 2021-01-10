using BepInEx;
using HarmonyLib;
using BepInEx.Configuration;

namespace configurable_storms
{
    [BepInPlugin("ConfigurableStorms", "Configurable Storms Mod", "1.0.0.0")]
    public class ConfigStormsPlugin : BaseUnityPlugin
    {

        public static bool HasWeather;
        public static int WeatherMinimumCooldownDuration;
        public static int WeatherMaximumCooldownDuration;
        public static float StormWindStrength;
        public static float StormGroundedMaxSpeedMultiplier;
        public static float SolarPanelHealthDamage;
        public static float DynamicThingHealthDamage;


        private ConfigEntry<bool> configHW;
        private ConfigEntry<int> configWMinCD;
        private ConfigEntry<int> configWMaxCD;
        private ConfigEntry<float> configSWS;
        private ConfigEntry<float> configSGMSM;
        private ConfigEntry<float> configSPHD;
        private ConfigEntry<float> configDTHD;


        public static void ModLog(string text)
        {
            UnityEngine.Debug.Log("[Configurable Storms] " + text);
        }

        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
            HandleConfig();
            ModLog("Patching...");
            var harmony = new Harmony("ConfigurableStorms");
            harmony.PatchAll();
            ModLog("Patched");
            ModLog("Successfully loaded Configurable Storms");
        }

        void HandleConfig()
        {
            configHW = Config.Bind("Settings",   // The section under which the option is shown
                                     "WeatherEnabled",  // The key of the configuration option in the configuration file
                                     true, // The default value
                                     "Enables weather on appropriate maps (default is true)"); // Description of the option to show in the config file

            configWMinCD = Config.Bind("Settings",   // The section under which the option is shown
                                     "WeatherMinimumCooldown",  // The key of the configuration option in the configuration file
                                     3, // The default value
                                     "Lower bound of storm cooldown"); // Description of the option to show in the config file

            configWMaxCD = Config.Bind("Settings",   // The section under which the option is shown
                                     "WeatherMaximumCooldown",  // The key of the configuration option in the configuration file
                                     12, // The default value
                                     "Upper bound of storm cooldown"); // Description of the option to show in the config file

            configSWS = Config.Bind("Settings",   // The section under which the option is shown
                                     "StormWindStrength",  // The key of the configuration option in the configuration file
                                     15f, // The default value
                                     "Strength of the storm's wind (default is 15"); // Description of the option to show in the config fil;

            configSGMSM = Config.Bind("Settings",   // The section under which the option is shown
                                     "StormGroundedMaxSpeedMultiplier",  // The key of the configuration option in the configuration file
                                     0.1f, // The default value
                                     "Max possible multiplier to player speed when on the ground (default is 0.1"); // Description of the option to show in the config fil;

            configSPHD = Config.Bind("Settings",   // The section under which the option is shown
                                     "SolarPanelHealthDamage",  // The key of the configuration option in the configuration file
                                     0.05f, // The default value
                                     "How much damage the storm does to solar panels (default is 0.05"); // Description of the option to show in the config fil;

            configDTHD = Config.Bind("Settings",   // The section under which the option is shown
                                     "DynamicThingHealthDamage",  // The key of the configuration option in the configuration file
                                     0.03f, // The default value
                                     "How much damage the storm does to dynamic things, e.g. objects (default is 0.03"); // Description of the option to show in the config fil;

            HasWeather = configHW.Value;
            ModLog(HasWeather.ToString());
            //DefaultCooldown = configDC.Value;
            StormWindStrength = configSWS.Value;
            StormGroundedMaxSpeedMultiplier = configSGMSM.Value;
            SolarPanelHealthDamage = configSPHD.Value;
            DynamicThingHealthDamage = configDTHD.Value;

        }
    }
}
