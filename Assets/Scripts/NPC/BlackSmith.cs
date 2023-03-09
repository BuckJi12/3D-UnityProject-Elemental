using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmith : NPC
{
    [SerializeField]
    private List<ItemData> itemDatas;
    [SerializeField]
    private GameObject shopElement;
    [SerializeField]
    private Transform shopPos;

    public void MakeShop()
    {
        for (int i = 0; i < itemDatas.Count; i++)
        {
            Shop shop = PoolManager.Instance.Get(shopElement).GetComponent<Shop>();
            shop.transform.SetParent(shopPos);
            shop.Set(itemDatas[i]);
        }
    }

    public override void InterAction()
    {
        MakeShop();
        UIManager.Instance.SwitchUI("Shop");
    }
}
