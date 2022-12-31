using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public new string name;
    public string description;
    public ItemKind kind;
    public int count;
    public int cost;

    public GameObject prefab;
    public Sprite itemIcon;
}
