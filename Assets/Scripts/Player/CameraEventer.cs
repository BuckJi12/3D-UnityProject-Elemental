using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraEventer : MonoBehaviour
{
    public UnityEvent cameraSwitch;

    public void CameraSwitch()
    {
        cameraSwitch?.Invoke();
    }
}
