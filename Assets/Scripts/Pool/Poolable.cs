using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    [SerializeField]
    private float returnTime;
    [SerializeField]
    private bool canReturn;


    private void OnEnable()
    {
        StartCoroutine(DelayToReturn());
    }

    private IEnumerator DelayToReturn()
    {
        if(canReturn)
        {
            yield return new WaitForSeconds(returnTime);
            PoolManager.Instance.Release(gameObject);
        }
    }
}
