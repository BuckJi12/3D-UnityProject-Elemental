using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [HideInInspector]
    public int damage;
    [SerializeField]
    private GameObject meteorEffect;

    private DryadMeteor dryadAttack;

    private void Awake()
    {
        dryadAttack = GetComponentInParent<DryadMeteor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            dryadAttack.Off();
            ParticleSystem particle = PoolManager.Instance.Get(meteorEffect).GetComponent<ParticleSystem>();
            particle.transform.position = transform.position;
            particle.Play();

            Collider[] colliders = Physics.OverlapSphere(transform.position, 3, LayerMask.GetMask("Player"));

            if (colliders == null)
                return;

            if (colliders.Length < 1)
                return;

            IDamageable damageable = colliders[0].GetComponent<IDamageable>();
            damageable?.TakeDamage(this.damage);
        }
    }
}
