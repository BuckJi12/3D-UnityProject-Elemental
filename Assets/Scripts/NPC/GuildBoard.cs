using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildBoard : NPC
{
    public override void Talk()
    {
        UIManager.Instance.SwitchUI("Quest");
    }
}
