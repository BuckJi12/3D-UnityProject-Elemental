using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/CollectedRequest")]
public class CollectedRequest : Quest
{
    public ItemData item;

    public override ItemData GetItemData()
    {
        return item;
    }
}
