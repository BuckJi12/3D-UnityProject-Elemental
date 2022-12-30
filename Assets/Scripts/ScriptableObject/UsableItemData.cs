using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/UsableItemData")]
public class UsableItemData : ScriptableObject
{
    public new string name;
    public string description;
    public ItemKind kind;
    public int count;
    public int cost;

    public int value;

    public GameObject prefab;
}
