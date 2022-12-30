using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ScriptableObject data;

    public void Pick(PlayerColliders collider)
    {
        Destroy(gameObject);
    }
}
