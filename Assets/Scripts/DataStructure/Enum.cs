using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemKind
{
    Equipment,
    UsableItem,
    Materialitem
}

public enum EquipmentKind
{
    Accessory,
    Weapon,
    Head,
    Chest,
    Legs,
    Feet,
    None
}

public enum ItemState
{
    DropItem,
    Wearing
}

public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Hit,
    Die
}

public enum BossState
{
    Idle,
    Move,
    Attack,
    SkillA,
    Hit,
    Die
}


[Flags]
public enum Elemental
{
    None        = 0000_0000_0000_0000,  // 1 << 1 == 0000_0000_0000_0000_0000_0000_0000_0001
    Ice         = 0000_0000_0000_0001,  // 1 << 0
    Fire        = 0000_0000_0000_0010,  // 1 << 1
    Lightning   = 0000_0000_0000_0100   // 1 << 2
}

