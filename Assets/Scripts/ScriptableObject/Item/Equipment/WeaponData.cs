using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/WeaponData")]
public class WeaponData : ItemData
{
    public int damage;
    public int elementalPower;

    public int criticalPercent;
    public int criticalDamage;
}
