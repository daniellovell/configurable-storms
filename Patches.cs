using System;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Voxel;
using HarmonyLib;

using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Assets.Scripts.Objects.Items;
using Weather;

namespace configurable_storms
{
    class Patches
    {
        /// <summary>
        /// Increasing drill speed
        /// </summary>
        [HarmonyPatch(typeof(WeatherManager), "Awake")]
        public class WeatherManager_Awake
        {
            static FieldInfo defaultCooldown = AccessTools.Field(typeof(WeatherManager), "_defaultCooldown");
            static FieldInfo stormWindStrength = AccessTools.Field(typeof(WeatherManager), "_stormWindStrength");
            static FieldInfo stormGroundedMaxSpeedMultiplier = AccessTools.Field(typeof(WeatherManager), "_stormGroundedMaxSpeedMultiplier");
            static FieldInfo dynamicThingsHealthDamage = AccessTools.Field(typeof(WeatherManager), "_dynamicThingHealthDamage");
            static FieldInfo solarPanelHealthDamage = AccessTools.Field(typeof(WeatherManager), "_solarPanelHealthDamage");

            static void Prefix(WeatherManager __instance)
            {
                //defaultCooldown.SetValue(__instance, ConfigStormsPlugin.DefaultCooldown);
                stormWindStrength.SetValue(__instance, ConfigStormsPlugin.StormWindStrength);
                stormGroundedMaxSpeedMultiplier.SetValue(__instance, ConfigStormsPlugin.StormGroundedMaxSpeedMultiplier);
                dynamicThingsHealthDamage.SetValue(__instance, ConfigStormsPlugin.DynamicThingHealthDamage);
                solarPanelHealthDamage.SetValue(__instance, ConfigStormsPlugin.SolarPanelHealthDamage);
            }
        }

        /// <summary>
        /// Min/max cooldown durations
        /// </summary>
        [HarmonyPatch(typeof(WeatherManager), "LoadWeatherEventSettings")]
        public class WeatherManager_LoadWeatherEventSettings
        {
            static PropertyInfo worldHasWeather = AccessTools.Property(typeof(WeatherManager), "WorldHasWeather");

            static void Postfix(WeatherManager __instance)
            {
                //defaultCooldown.SetValue(__instance, ConfigStormsPlugin.DefaultCooldown);
                if(WeatherManager.Instance != null && WeatherManager.CurrentWeatherSetting != null)
                {
                    WeatherManager.CurrentWeatherSetting.WeatherMinimumCooldownDuration = ConfigStormsPlugin.WeatherMinimumCooldownDuration;
                    WeatherManager.CurrentWeatherSetting.WeatherMaximumCooldownDuration = ConfigStormsPlugin.WeatherMaximumCooldownDuration;
                }
            }
        }

        [HarmonyPatch(typeof(WeatherManager), "WorldHasWeather", MethodType.Getter)]
        public class WeatherManager_WorldHasWeatherGetter
        {
            static void Postfix(ref bool __result)
            {
                if (__result == true)
                {
                    __result = ConfigStormsPlugin.HasWeather;
                }
            }
        }

        /// <summary>
        /// For debugging
        /// </summary>
        [HarmonyPatch(typeof(WeatherManager), "OnWorldStart")]
        public class WeatherManager_StartScheduleServer
        {

            static void Postfix(WeatherManager __instance)
            {
                //ConfigStormsPlugin.ModLog(WeatherManager.WeatherStartTime.ToString());
                //ConfigStormsPlugin.ModLog(WeatherManager.IsWeatherEventScheduled.ToString());
            }
        }
    }
}
