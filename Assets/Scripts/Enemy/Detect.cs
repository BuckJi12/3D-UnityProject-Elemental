using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detect : MonoBehaviour
{
    private SphereCollider detectRange;

    public bool isDetect = false;

    private void Awake()
    {
        detectRange = GetComponent<SphereCollider>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isDetect = true;
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
        isDetect = false;
    }
}
