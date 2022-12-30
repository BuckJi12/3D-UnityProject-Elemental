using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/EquipmentData")]
public class EquipmentData : ScriptableObject
{
    public new string name;
    public string description;
    public ItemKind kind;
    public EquipmentKind equipKind;
    public int cost;

    public int damage;

    public GameObject prefab;
}
