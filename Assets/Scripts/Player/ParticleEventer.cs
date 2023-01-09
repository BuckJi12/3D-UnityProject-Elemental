using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleEventer : MonoBehaviour
{
    public UnityEvent iceBreak;
    public UnityEvent fireDischarge;

    public void OnIceBreakParticle()
    {
        iceBreak?.Invoke();     
    }
    public void OnFireDischargeParticle()
    {
        fireDischarge?.Invoke();
    }
}
