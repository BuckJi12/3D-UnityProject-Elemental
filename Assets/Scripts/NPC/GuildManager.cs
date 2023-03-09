using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildManager : NPC
{
    public override void Talk()
    {
        UIManager.Instance.SwitchUI("CompleteQuest");
    }
}
