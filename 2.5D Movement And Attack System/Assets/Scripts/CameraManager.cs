using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraManager
{
    public static void ChangeActiveCamera(CinemachineVirtualCamera newCamera)
    {
        (GameObject.FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera).enabled = false;
        newCamera.enabled = true;
    }
}
