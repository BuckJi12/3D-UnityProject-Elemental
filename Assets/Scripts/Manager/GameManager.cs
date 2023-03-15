using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    public int money;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            money += 100000;
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            PlayerStatManager.Instance.CalculateEXP((int)PlayerStatManager.Instance.stat.maxEXP);
        }
    }
}
