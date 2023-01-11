using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    [SerializeField]
    private GameObject iceBreak;
    [SerializeField]
    private GameObject fireDischarge;


    public void OnParticleIceBreak()
    {
        ParticleSystem instance = PoolManager.Instance.Get(iceBreak).GetComponent<ParticleSystem>();
        instance.transform.position = transform.position;
        instance.transform.rotation = transform.rotation;
        instance.transform.Rotate(Vector3.up * -90);
        instance.Play();
    }

    public void OnParticleFireDischarge()
    {
        ParticleSystem instance = PoolManager.Instance.Get(fireDischarge).GetComponent<ParticleSystem>();
        instance.transform.position = transform.position;
        instance.Play();
    }
}
