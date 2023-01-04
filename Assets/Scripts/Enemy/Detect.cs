using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Detect : MonoBehaviour
{
    public SphereCollider detectRange;

    //public bool isDetect = false;
    public UnityEvent modeSwitch;

    private void Awake()
    {
        detectRange = GetComponent<SphereCollider>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            modeSwitch?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            StartCoroutine(DetectCancel());
        }
    }

    private IEnumerator DetectCancel()
    {
        yield return new WaitForSeconds(3);
        modeSwitch?.Invoke();
    }
}
