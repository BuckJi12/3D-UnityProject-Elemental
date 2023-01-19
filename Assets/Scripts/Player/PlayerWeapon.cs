using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Weapon curWeapon = null;


    public void SwitchWeapon()
    {
        curWeapon = GetComponentInChildren<Weapon>();
        
        if (curWeapon == null)
            return;
    }

    public void StartAttack()
    {
        curWeapon.OnCollider();
    }

    public void EndAttack()
    {
        curWeapon.OffCollider();
    }

    public void StartTrail()
    {
        curWeapon.OnTrail();
    }

    public void EndTrail()
    {
        curWeapon.OffTrail();
    }
}
