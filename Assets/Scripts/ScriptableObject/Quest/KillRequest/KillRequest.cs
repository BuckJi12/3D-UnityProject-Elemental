using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/KillRequest")]
public class KillRequest : Quest
{
    public MonsterData target;
    public int goalCatches;
    private int catches;


    public override void Progress(Enemy enemy)
    {
        if (target.name != enemy.data.name)
            return;

        if (canComplete)
            return;

        catches++;
        Check();
    }

    public override void Check()
    {
        if (catches >= goalCatches)
        {
            this.canComplete = true;
        }
        else
        {
            this.canComplete = false;
        }
    }
}
