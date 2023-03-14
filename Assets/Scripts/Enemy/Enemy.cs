using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rigid;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public CapsuleCollider myCollider;

    public Dictionary<EnemyState, State<Enemy>> state;
    public StateMachine<Enemy> machine;

    [HideInInspector]
    public GameObject target;

    [HideInInspector]
    public EnemyData data;
    [HideInInspector]
    public ElementalReation ele;

    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject armor;
    [SerializeField]
    private GameObject material;

    public virtual void Attack()
    {

    }

    private void OnEnable()
    {
        Respawn();
    }

    private void Update()
    {
        machine.Update();
    }

    public void ChangeState(State<Enemy> newState)
    {
        machine.ChangeState(newState);
    }

    public void ChangeBeforeState()
    {
        machine.ChangeBefore();
    }

    public void Die()
    {
        data.isAlive = false;
        myCollider.enabled = false;
    }

    public void Respawn()
    {
        myCollider.enabled = true;
        ChangeState(state[EnemyState.Idle]);
        data.Init();
    }

    public void DropMoney()
    {
        GameObject instance = PoolManager.Instance.Get(coin);
        Money money = instance.GetComponent<Money>();
        money.SetMoney(data.monster.money);
        instance.transform.position = data.damagePos.transform.position;
    }

    public void DropItem()
    {
        if (data.monster.dropItems != null)
        {
            for (int i = 0; i < data.monster.dropItems.Count; i++)
            {
                int random = Random.Range(1, 100);
                if (data.monster.dropItems[i].dropRate >= random)
                {
                    ItemProp prop = CreateItem(data.monster.dropItems[i].itemData).GetComponent<ItemProp>();
                    prop.Set(data.monster.dropItems[i].itemData);
                    prop.transform.position = transform.position;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public GameObject CreateItem(ItemData data)
    {
        if (data.kind == ItemKind.Equipment)
        {
            if (data.equipKind == EquipmentKind.Weapon)
            {
                return PoolManager.Instance.Get(weapon);
            }
            else
            {
                return PoolManager.Instance.Get(armor);
            }
        }
        else
        {
            return PoolManager.Instance.Get(material);
        }
    }
}
