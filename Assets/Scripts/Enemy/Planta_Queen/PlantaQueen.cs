using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaQueen : Enemy
{
    [SerializeField]
    private GameObject energyBall;
    [SerializeField]
    private Transform attackStartPos;

    public void Attack()
    {
        EnergyBall energyBall = PoolManager.Instance.Get(this.energyBall).GetComponent<EnergyBall>();
        energyBall.transform.position = attackStartPos.position;
        energyBall.Set(this.damage, this.target);
    }
}
