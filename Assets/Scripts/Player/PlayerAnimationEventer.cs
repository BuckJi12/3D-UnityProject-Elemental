using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEventer : MonoBehaviour
{
    public UnityEvent OnStartAttack;
    public UnityEvent OnEndAttack;
    public UnityEvent OnStartTrail;
    public UnityEvent OnEndTrail;
    public UnityEvent OnCanMove;
    public UnityEvent OnInvincibility;

    public void StartAttack()
    {
        OnStartAttack?.Invoke();
    }

    public void EndAttack()
    {
        OnEndAttack?.Invoke();
    }

    public void StartTrail()
    {
        OnStartTrail?.Invoke();
    }

    public void EndTrail()
    {
        OnEndTrail?.Invoke();
    }

    public void SwitchCanMove()
    {
        OnCanMove?.Invoke();
    }

    public void SwitchOnInvincibility()
    {
        OnInvincibility?.Invoke();
    }
}
