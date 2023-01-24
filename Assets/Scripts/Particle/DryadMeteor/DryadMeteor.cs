using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DryadMeteor : MonoBehaviour
{
    [SerializeField]
    private GameObject hitPostion;
    private Meteor fallObject;
    private void Awake()
    {
        fallObject = GetComponentInChildren<Meteor>();
    }

    private void OnEnable()
    {
        hitPostion.SetActive(true);
        fallObject.gameObject.SetActive(true);
    }

    public void Set(int damage)
    {
        fallObject.damage = damage;

        int random1 = Random.Range(-10, 10);
        int random2 = Random.Range(-10, 10);
        //Vector3 hitPos = new Vector3(random1, 0, random2);
        Vector3 fallPos = new Vector3(random1, 20, random2);

        //hitPostion.transform.position = hitPostion.transform.position + hitPos;
        fallObject.transform.localPosition = fallPos;

        Ray ray = new Ray(fallObject.transform.position, -transform.up);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 30, LayerMask.GetMask("Ground")))
        {
            hitPostion.transform.position = hitData.point;
        }
    }

    public void Off()
    {
        hitPostion.SetActive(false);
        fallObject.gameObject.SetActive(false);
    }
}
