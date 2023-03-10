using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private Rigidbody rigid;
    private BoxCollider boxCollider;
    private CapsuleCollider capsuleCollider;
    private TrailRenderer trail;

    private ItemState itemState;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        capsuleCollider.enabled = false;
        itemState = ItemState.DropItem;
        trail.enabled = false;
    }

    public void ItemMode()
    {
        boxCollider.enabled = true;
        rigid.useGravity = true;
        itemState = ItemState.DropItem;
    }

    public void WearingMode()
    {
        boxCollider.enabled = false;
        rigid.useGravity = false;
        itemState = ItemState.Wearing;
    }

    public void SwitchMode()
    {
        switch (itemState)
        {
            case ItemState.DropItem:
                WearingMode();
                break;
            case ItemState.Wearing:
                ItemMode();
                break;
        }
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
