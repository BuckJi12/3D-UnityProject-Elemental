using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Camera skillCamera;
    [SerializeField]
    private CinemachineFreeLook cinCam;


    private void Update()
    {
        if (UIManager.Instance.uisList.Count > 0 || UIManager.Instance.isUsingMouse)
            cinCam.enabled = false;
        else
            cinCam.enabled = true;
    }

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
