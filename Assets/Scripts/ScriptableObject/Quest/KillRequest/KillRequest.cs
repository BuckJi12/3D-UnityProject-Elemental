using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/KillRequest")]
public class KillRequest : Quest
{
    public MonsterData target;
    public int goalCatches;
    public int catches;


    public override void Kill(Enemy enemy)
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
            Debug.Log("퀘스트완료");
        }
        else
        {
            this.canComplete = false;
        }
    }
}
