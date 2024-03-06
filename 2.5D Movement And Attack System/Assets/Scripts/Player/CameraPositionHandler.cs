using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem.HID;

public class CameraPositionHandler : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] Transform dynamicCamera;

    bool cameraCollided = false;

    private void Update()
    {
        if(Physics.Raycast(transform.position, mainCamera.position - transform.position, out RaycastHit hit, Vector3.Distance(transform.position, mainCamera.position)) && hit.transform != transform)
        {
            dynamicCamera.position = hit.point;
            CameraManager.ChangeActiveCamera(dynamicCamera.GetComponent<CinemachineVirtualCamera>());
            cameraCollided = true;
            return;
        }
        if(cameraCollided)
        {
            cameraCollided = false;
            CameraManager.ChangeActiveCamera(mainCamera.GetComponent<CinemachineVirtualCamera>());
        }
    }
}
