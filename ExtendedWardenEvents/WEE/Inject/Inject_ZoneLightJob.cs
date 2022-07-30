﻿using ExtendedWardenEvents.WEE.Replicators;
using HarmonyLib;
using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedWardenEvents.WEE.Inject
{
    [HarmonyPatch(typeof(LG_BuildZoneLightsJob), nameof(LG_BuildZoneLightsJob.Build))]
    internal static class Inject_ZoneLightJob
    {
        private static void Prefix(LG_BuildZoneLightsJob __instance)
        {
            var zone = __instance.m_zone;
            if (zone == null)
                return;

            if (zone.gameObject.GetComponent<ZoneLightReplicator>() == null)
            {
                var replicator = zone.gameObject.AddComponent<ZoneLightReplicator>();
                replicator.Setup(zone);
            }
        }

        private static void Postfix(bool __result, LG_BuildZoneLightsJob __instance)
        {
            if (!__result)
                return;

            var zone = __instance.m_zone;
            if (zone == null)
                return;

            var replicator = zone.gameObject.GetComponent<ZoneLightReplicator>();
            if (replicator != null)
            {
                replicator.Setup_UpdateLightSetting();
            }
        }
    }
}
