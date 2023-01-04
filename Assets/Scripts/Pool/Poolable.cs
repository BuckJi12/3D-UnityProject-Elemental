using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    [SerializeField]
    private float returnTime;


    private void OnEnable()
    {
        StartCoroutine(DelayToReturn());
    }

    private IEnumerator DelayToReturn()
    {
        yield return new WaitForSeconds(returnTime);
        PoolManager.Instance.Release(gameObject);
    }
}
