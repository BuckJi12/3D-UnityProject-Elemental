using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public new string name;
    public string description;
    public ItemKind kind;
    public ItemRank rank;
    public EquipmentKind equipKind;
    public int cost;
    public bool usable;

    public GameObject prefab;
    public Sprite itemIcon;
}
