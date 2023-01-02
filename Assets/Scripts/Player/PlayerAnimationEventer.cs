using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEventer : MonoBehaviour
{
    public UnityEvent OnStartAttack;
    public UnityEvent OnEndAttack;

    public void StartAttack()
    {
        OnStartAttack?.Invoke();
    }

    public void EndAttack()
    {
        OnEndAttack?.Invoke();
    }
}
