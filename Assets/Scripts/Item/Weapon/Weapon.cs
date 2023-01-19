using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private CapsuleCollider capsuleCollider;
    private TrailRenderer trail;

    private ItemState itemState;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        capsuleCollider.enabled = false;
        trail.enabled = false;
    }

    public void OnCollider()
    {
        capsuleCollider.enabled = true;
    }

    public void OffCollider()
    {
        capsuleCollider.enabled = false;
    }

    public void OnTrail()
    {
        trail.enabled = true;
    }

    public void OffTrail()
    {
        trail.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Monster"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage();
        }
    }
}
