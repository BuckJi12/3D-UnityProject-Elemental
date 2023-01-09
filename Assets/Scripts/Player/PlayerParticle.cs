using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem iceBreak;
    [SerializeField]
    private ParticleSystem fireDischarge;


    public void OnParticle(ParticleSystem particle)
    {
        particle.Play();
    }
}
