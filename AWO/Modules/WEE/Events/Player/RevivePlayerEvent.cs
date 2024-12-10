﻿using Agents;
using Player;

namespace AWO.Modules.WEE.Events;

internal sealed class RevivePlayerEvent : BaseEvent
{
    public override WEE_Type EventType => WEE_Type.RevivePlayer;

    protected override void TriggerMaster(WEE_EventData e)
    {
        var activeSlotIndices = new HashSet<int>(e.RevivePlayer.PlayerFilter.Select(filter => (int)filter));

        for (int i = 0; i < PlayerManager.PlayerAgentsInLevel.Count; i++)
        {
            PlayerAgent player = PlayerManager.PlayerAgentsInLevel[i];
            if (activeSlotIndices.Contains(i) && !player.Alive)
            {
                AgentReplicatedActions.PlayerReviveAction(player, player, player.Position);
            }
        }
    }
}