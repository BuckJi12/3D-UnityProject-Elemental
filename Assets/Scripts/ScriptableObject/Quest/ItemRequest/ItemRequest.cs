using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRequest : Quest
{
    public List<GoalItem> goalItems = new List<GoalItem>();

    public struct GoalItem
    {
        public ItemData item;
        public int curCount;
        public int goalCount;
        public bool collected;
    }

    public override void PickUp()
    {
        Check();
        CanComplete();
    }

    public override void Check()
    {
        for (int i = 0; i < goalItems.Count; i++)
        {
            if (InventoryManager.Instance.FindItem(goalItems[i].item) >= goalItems[i].goalCount)
            {
                GoalItem temp = goalItems[i];
                temp.curCount = InventoryManager.Instance.FindItem(goalItems[i].item);
                temp.collected = true;
                goalItems[i] = temp;
            }
            else
            {
                GoalItem temp = goalItems[i];
                temp.curCount = InventoryManager.Instance.FindItem(goalItems[i].item);
                temp.collected = false;
                goalItems[i] = temp;
            }
        }
    }

    public bool CanComplete()
    {
        for (int i = 0; i < goalItems.Count; i++)
        {
            if (goalItems[i].collected == true)
                continue;
            else
                return canComplete = false;
        }

        return canComplete = true;
    }
}
