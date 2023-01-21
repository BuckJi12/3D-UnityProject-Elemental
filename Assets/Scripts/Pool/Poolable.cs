using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    [SerializeField]
    private float returnTime;
    [SerializeField]
    private bool autoReturn;


    private void OnEnable()
    {
        StartCoroutine(DelayToReturn());
    }

    private IEnumerator DelayToReturn()
    {
        if(autoReturn)
        {
            yield return new WaitForSeconds(returnTime);
            PoolManager.Instance.Release(gameObject);
        }
    }

    public void ReturnPool()
    {
        if (autoReturn)
        {
            PoolManager.Instance.Release(gameObject);
        }
    }
}
