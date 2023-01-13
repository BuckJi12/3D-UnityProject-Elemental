using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turnipa : Enemy
{
    

    public void Attack()
    {
        if (this.isAlive)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, this.attackRange, LayerMask.GetMask("Player"));

            if (colliders == null)
                return;

            if (colliders.Length < 1)
                return;

            IDamageable damageable = target.GetComponent<IDamageable>();
            damageable?.TakeDamage(this.damage);
        }
    }
}
