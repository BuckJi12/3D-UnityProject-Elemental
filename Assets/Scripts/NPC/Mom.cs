using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : NPC
{
    [SerializeField]
    private List<ItemData> rewards;

    public bool oneChange = false;

    public override void InterAction()
    {
        if (!oneChange)
        {
            for (int i = 0; i < rewards.Count; i++)
            {
                InventoryItem item = new InventoryItem();
                item.data = rewards[i];
                InventoryManager.Instance.AddItem(item);
            }
            oneChange = true;
        }
    }
}
