using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Camera skillCamera;


    public void SwapCamera()
    {
        if (mainCamera.isActiveAndEnabled)
        {
            mainCamera.gameObject.SetActive(false);
            skillCamera.gameObject.SetActive(true);
        }
        else
        {
            mainCamera.gameObject.SetActive(true);
            skillCamera.gameObject.SetActive(false);
        }
    }
}
