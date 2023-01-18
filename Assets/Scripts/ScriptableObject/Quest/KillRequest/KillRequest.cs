using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/KillRequest")]
public class KillRequest : Quest
{
    public MonsterData target;

    public override MonsterData GetTargetData()
    {
        return target;
    }
}
