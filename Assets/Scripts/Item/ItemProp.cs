using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemProp : MonoBehaviour
{
    private ParticleSystem.MainModule particle;
    private Item item;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>().main;
        item = GetComponent<Item>();
    }


    public void Set(ItemData data)
    {
        item.data = data;
        switch (data.rank)
        {
            case ItemRank.Normal:
                particle.startColor = new Color(255, 255, 255);
                break;
            case ItemRank.Rare:
                particle.startColor = new Color(160, 255, 255);
                break;
            case ItemRank.Unique:
                particle.startColor = new Color(255, 54, 188);
                break;
            case ItemRank.Legendary:
                particle.startColor = new Color(255, 215, 0);
                break;
        }
    }
}
