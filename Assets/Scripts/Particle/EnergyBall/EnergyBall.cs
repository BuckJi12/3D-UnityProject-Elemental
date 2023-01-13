using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnergyBall : MonoBehaviour
{
    private Rigidbody rigid;
    private int damage;

    public float moveSpeed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    public void Set(int damage, GameObject myself)
    {
        this.damage = damage;
        transform.rotation = myself.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            this.gameObject.SetActive(false);
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(this.damage);
        }
    }
}
