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
                particle.startColor = new Color(160/255f, 255/255f, 255/255f);
                break;
            case ItemRank.Unique:
                particle.startColor = new Color(255 / 255f, 54 / 255f, 188 / 255f);
                break;
            case ItemRank.Legendary:
                particle.startColor = new Color(255 / 255f, 215 / 255f, 0 / 255f);
                break;
        }
    }
}
