using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParticle : MonoBehaviour
{
    private ParticleSystem.MainModule particle;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>().main;
    }

    public void Set()
    {
        particle.startColor = new Color(0,0,0);     
    }
}
