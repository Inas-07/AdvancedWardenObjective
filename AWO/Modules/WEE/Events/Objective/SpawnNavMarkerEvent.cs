﻿using AWO.WEE.Events;
using GTFO.API;
using LevelGeneration;
using SNetwork;
using UnityEngine;

namespace AWO.Modules.WEE.Events.Objective;

internal class SpawnNavMarkerEvent : BaseEvent
{
    public override WEE_Type EventType => WEE_Type.SpawnNavMarker;

    protected override void TriggerMaster(WEE_EventData e)
    {
        var name = $"AMAWO_{e.Count}";
        var marker = EntryPoint.NavMarkers.FirstOrDefault(go => go.name == name);

        if (marker == null)
        {
            var nav = new GameObject().AddComponent<LG_WorldEventNavMarker>();
            nav.name = name;
            nav.transform.position = e.Position;
            nav.m_placeNavMarkerOnGo.type = PlaceNavMarkerOnGO.eMarkerType.Guidance;
            nav.m_placeNavMarkerOnGo.m_placeOnStart = true;
            nav.SetupWorldEventComponent();

            EntryPoint.NavMarkers.Add(nav);
            LevelAPI.OnLevelCleanup += OnLevelCleanup;
            marker = nav;
        }

        if (e.Enabled)
            marker.ShowMarker();
        else
            marker.HideMarker();
    }

    private static void OnLevelCleanup()
    {
        Logger.Debug($"AdvancedWardenObjective - Cleaning up Nav Markers...");
        EntryPoint.NavMarkers.Clear();
    }
}
