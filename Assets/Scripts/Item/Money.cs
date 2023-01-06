using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int money;

    public void SetMoney(int money)
    {
        this.money = money;
    }

    public void Pick()
    {
        GameManager.Instance.money += money;
        PoolManager.Instance.Release(this.gameObject);
    }
}
