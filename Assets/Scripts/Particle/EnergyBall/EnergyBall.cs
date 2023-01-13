using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnergyBall : MonoBehaviour
{
    private NavMeshAgent agent;
    private int damage;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Set(int damage, GameObject target)
    {
        this.damage = damage;
        agent.SetDestination(target.transform.position);
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
