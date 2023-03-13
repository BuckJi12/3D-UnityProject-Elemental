using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Detect : MonoBehaviour
{
    [HideInInspector]
    public SphereCollider detectRange;

    private Enemy enemy;

    private void Awake()
    {
        detectRange = GetComponent<SphereCollider>();
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            enemy.data.findTarget = true;
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
        enemy.data.findTarget = false;
    }
}
