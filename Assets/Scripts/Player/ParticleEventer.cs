using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleEventer : MonoBehaviour
{
    public UnityEvent OnParticle;

    public void OnParticleFunc()
    {
        OnParticle?.Invoke();     
    }
}
