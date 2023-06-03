using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

public class CamSwitcher : MonoBehaviour
{
    private ControlScheme _controlScheme;
    private InputAction _switchToCam1;
    private InputAction _switchToCam2;
    private InputAction _switchToCam3;
    private InputAction _switchToCam4;
    private InputAction _switchToCam5;
    private InputAction _switchToCam6;
    private InputAction _toggleFreeCam;
    private InputAction _setFreeCamTo1;
    private InputAction _setFreeCamTo2;
    private InputAction _setFreeCamTo3;
    private InputAction _setFreeCamTo4;
    private InputAction _setFreeCamTo5;
    private InputAction _setFreeCamTo6;

    [SerializeField] private List<bool> activeCameraStatus;

    //private bool _freecam = false;
    //private bool _cam1 = true;
    //private bool _cam2 = false;
    //private bool _cam3 = false;
    //private bool _cam4 = false;
    //private bool _cam5 = false;
    //private bool _cam6 = false;

    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras;

    //[BoxGroup("Virtual Cameras")][SerializeField] private CinemachineVirtualCamera vcam1;
    //[BoxGroup("Virtual Cameras")][SerializeField] private CinemachineVirtualCamera vcam2;
    //[BoxGroup("Virtual Cameras")][SerializeField] private CinemachineVirtualCamera vcam3;
    //[BoxGroup("Virtual Cameras")][SerializeField] private CinemachineVirtualCamera vcam4;
    //[BoxGroup("Virtual Cameras")][SerializeField] private CinemachineVirtualCamera vcam5;
    //[BoxGroup("Virtual Cameras")][SerializeField] private CinemachineVirtualCamera vcam6;
    //[BoxGroup("Virtual Cameras")][SerializeField] private CinemachineVirtualCamera freecam;

    [SerializeField] private List<Transform> CameraTransforms; 

    //[SerializeField] private GameObject cam1Pos;
    //[SerializeField] private GameObject cam2Pos;
    //[SerializeField] private GameObject cam3Pos;
    //[SerializeField] private GameObject cam4Pos;
    //[SerializeField] private GameObject cam5Pos;
    //[SerializeField] private GameObject cam6Pos;
    //[SerializeField] private GameObject freecamPos;

    [Dropdown("cameraValues")]
    public int cameraValue;

    [SerializeField] private List<int> cameraValues/* = new int[] { 1, 2, 3, 4, 5, 6, 7}*/;

    private void Awake()
    {
        _controlScheme = new ControlScheme();
    }

    private void OnEnable()
    {
        _switchToCam1 = _controlScheme.Camera.SwitchToCam1;
        _switchToCam2 = _controlScheme.Camera.SwitchToCam2;
        _switchToCam3 = _controlScheme.Camera.SwitchToCam3;
        _switchToCam4 = _controlScheme.Camera.SwitchToCam4;
        _switchToCam5 = _controlScheme.Camera.SwitchToCam5;
        _switchToCam6 = _controlScheme.Camera.SwitchToCam6;
        _toggleFreeCam = _controlScheme.Camera.ToggleFreeCam;
        _setFreeCamTo1 = _controlScheme.Camera.SetFreeCamTo1;
        _setFreeCamTo2 = _controlScheme.Camera.SetFreeCamTo2;
        _setFreeCamTo3 = _controlScheme.Camera.SetFreeCamTo3;
        _setFreeCamTo4 = _controlScheme.Camera.SetFreeCamTo4;
        _setFreeCamTo5 = _controlScheme.Camera.SetFreeCamTo5;
        _setFreeCamTo6 = _controlScheme.Camera.SetFreeCamTo6;
        _controlScheme.Camera.Enable();
    }

    private void OnDisable()
    {
        _controlScheme.Camera.Disable();
    }

    private void FixedUpdate()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        _switchToCam1.performed += context => { SwitchCamera(1); };
        _switchToCam2.performed += context => { SwitchCamera(2); };
        _switchToCam3.performed += context => { SwitchCamera(3); };
        _switchToCam4.performed += context => { SwitchCamera(4); };
        _switchToCam5.performed += context => { SwitchCamera(5); };
        _switchToCam6.performed += context => { SwitchCamera(6); };
        _toggleFreeCam.started += context => { SwitchCamera(0); };
        _setFreeCamTo1.performed += context => { SetCameraToFreeCam(1); };
        _setFreeCamTo2.performed += context => { SetCameraToFreeCam(2); };
        _setFreeCamTo3.performed += context => { SetCameraToFreeCam(3); };
        _setFreeCamTo4.performed += context => { SetCameraToFreeCam(4); };
        _setFreeCamTo5.performed += context => { SetCameraToFreeCam(5); };
        _setFreeCamTo6.performed += context => { SetCameraToFreeCam(6); };
    }

    private void SwitchCamera(int idx) 
    {
        Debug.Log("IDX: " + idx);
        if (idx == 0)
        {
            if (activeCameraStatus[0])
            {
                for (int i = 0; i < 7; i++)
                {
                    activeCameraStatus[i] = false;
                    virtualCameras[i].Priority = 0;
                }

                activeCameraStatus[1] = true;
                virtualCameras[1].Priority = 1;
            }
            else
            {
                if (activeCameraStatus[1])
                {
                    CameraTransforms[0].position = virtualCameras[1].transform.position;
                    CameraTransforms[0].rotation = virtualCameras[1].transform.rotation;
                }
                else if (activeCameraStatus[2])
                {
                    CameraTransforms[0].position = virtualCameras[2].transform.position;
                    CameraTransforms[0].rotation = virtualCameras[2].transform.rotation;
                }
                else if (activeCameraStatus[3])
                {
                    CameraTransforms[0].position = virtualCameras[3].transform.position;
                    CameraTransforms[0].rotation = virtualCameras[3].transform.rotation;
                }
                else if (activeCameraStatus[4])
                {
                    CameraTransforms[0].position = virtualCameras[4].transform.position;
                    CameraTransforms[0].rotation = virtualCameras[4].transform.rotation;
                }
                else if (activeCameraStatus[5])
                {
                    CameraTransforms[0].position = virtualCameras[5].transform.position;
                    CameraTransforms[0].rotation = virtualCameras[5].transform.rotation;
                }
                else if (activeCameraStatus[6])
                {
                    CameraTransforms[0].position = virtualCameras[6].transform.position;
                    CameraTransforms[0].rotation = virtualCameras[6].transform.rotation;
                }

                for (int i = 1; i < 7; i++)
                {
                    activeCameraStatus[i] = false;
                    virtualCameras[i].Priority = 0;
                }

                activeCameraStatus[idx] = true;
                virtualCameras[idx].Priority = 1;
            }
        }
        else 
        {
            for (int i = 0; i < 7; i++) 
            {
                activeCameraStatus[i] = false;
                virtualCameras[i].Priority = 0;
            }

            activeCameraStatus[idx] = true;
            virtualCameras[idx].Priority = 1;  
        }
    }
    private void SetCameraToFreeCam(int idx) 
    {
        if (!activeCameraStatus[0])
            return;

        CameraTransforms[idx].position = CameraTransforms[0].position;
        virtualCameras[idx].transform.rotation = virtualCameras[0].transform.rotation;
    }
    [Button]
    public void SetCameraFromFreeCam() 
    {
        SetCameraToFreeCam(cameraValue);
    }
    [Button]
    public void SwitchToCameraNumber() 
    {
        SwitchCamera(cameraValue);
    }

    //[Button]
    //public void SwitchToCamera1()
    //{
    //    if (!_cam1)
    //    {
    //        vcam1.Priority = 1;
    //        vcam2.Priority = 0;
    //        vcam3.Priority = 0;
    //        vcam4.Priority = 0;
    //        vcam5.Priority = 0;
    //        vcam6.Priority = 0;
    //        freecam.Priority = 0;
    //    }
    //    _cam1 = true;
    //    _cam2 = false;
    //    _cam3 = false;
    //    _cam4 = false;
    //    _cam5 = false;
    //    _cam6 = false;
    //    _freecam = false;
    //}

    //[Button]
    //public void SwitchToCamera2()
    //{
    //    if (!_cam1)
    //    {
    //        vcam1.Priority = 0;
    //        vcam2.Priority = 1;
    //        vcam3.Priority = 0;
    //        vcam4.Priority = 0;
    //        vcam5.Priority = 0;
    //        vcam6.Priority = 0;
    //        freecam.Priority = 0;
    //    }
    //    _cam1 = false;
    //    _cam2 = true;
    //    _cam3 = false;
    //    _cam4 = false;
    //    _cam5 = false;
    //    _cam6 = false;
    //    _freecam = false;
    //}
    
    //[Button]
    //public void SwitchToCamera3()
    //{
    //    if (!_cam1)
    //    {
    //        vcam1.Priority = 0;
    //        vcam2.Priority = 0;
    //        vcam3.Priority = 1;
    //        vcam4.Priority = 0;
    //        vcam5.Priority = 0;
    //        vcam6.Priority = 0;
    //        freecam.Priority = 0;
    //    }
    //    _cam1 = false;
    //    _cam2 = false;
    //    _cam3 = true;
    //    _cam4 = false;
    //    _cam5 = false;
    //    _cam6 = false;
    //    _freecam = false;
    //}
    
    //[Button]
    //public void SwitchToCamera4()
    //{
    //    if (!_cam1)
    //    {
    //        vcam1.Priority = 0;
    //        vcam2.Priority = 0;
    //        vcam3.Priority = 0;
    //        vcam4.Priority = 1;
    //        vcam5.Priority = 0;
    //        vcam6.Priority = 0;
    //        freecam.Priority = 0;
    //    }
    //    _cam1 = false;
    //    _cam2 = false;
    //    _cam3 = false;
    //    _cam4 = true;
    //    _cam5 = false;
    //    _cam6 = false;
    //    _freecam = false;
    //}
    
    //[Button]
    //public void SwitchToCamera5()
    //{
    //    if (!_cam1)
    //    {
    //        vcam1.Priority = 0;
    //        vcam2.Priority = 0;
    //        vcam3.Priority = 0;
    //        vcam4.Priority = 0;
    //        vcam5.Priority = 1;
    //        vcam6.Priority = 0;
    //        freecam.Priority = 0;
    //    }
    //    _cam1 = false;
    //    _cam2 = false;
    //    _cam3 = false;
    //    _cam4 = false;
    //    _cam5 = true;
    //    _cam6 = false;
    //    _freecam = false;
    //}
    
    //[Button]
    //public void SwitchToCamera6()
    //{
    //    if (!_cam1)
    //    {
    //        vcam1.Priority = 0;
    //        vcam2.Priority = 0;
    //        vcam3.Priority = 0;
    //        vcam4.Priority = 0;
    //        vcam5.Priority = 0;
    //        vcam6.Priority = 1;
    //        freecam.Priority = 0;
    //    }
    //    _cam1 = false;
    //    _cam2 = false;
    //    _cam3 = false;
    //    _cam4 = false;
    //    _cam5 = false;
    //    _cam6 = true;
    //    _freecam = false;
    //}

    //[Button]
    //public void FreeCameraToggle()
    //{
    //    Debug.Log("This should also appear." + _freecam);
    //    if (_freecam)
    //    {
    //        vcam1.Priority = 1;
    //        vcam2.Priority = 0;
    //        vcam3.Priority = 0;
    //        vcam4.Priority = 0;
    //        vcam5.Priority = 0;
    //        vcam6.Priority = 0;
    //        freecam.Priority = 0;
            
    //        _cam1 = true;
    //        _cam2 = false;
    //        _cam3 = false;
    //        _cam4 = false;
    //        _cam5 = false;
    //        _cam6 = false;
    //        _freecam = false;
    //    }
    //    else if (!_freecam)
    //    {
    //        if (_cam1)
    //        {
    //            freecamPos.transform.position = vcam1.transform.position;
    //            freecam.transform.rotation = vcam1.transform.rotation;
    //        }
    //        else if (_cam2)
    //        {
    //            freecamPos.transform.position = vcam2.transform.position;
    //            freecam.transform.rotation = vcam2.transform.rotation;
    //        }
    //        else if (_cam3)
    //        {
    //            freecamPos.transform.position = vcam3.transform.position;
    //            freecam.transform.rotation = vcam3.transform.rotation;
    //        }
    //        else if (_cam4)
    //        {
    //            freecamPos.transform.position = vcam4.transform.position;
    //            freecam.transform.rotation = vcam4.transform.rotation;
    //        }
    //        else if (_cam5)
    //        {
    //            freecamPos.transform.position = vcam5.transform.position;
    //            freecam.transform.rotation = vcam5.transform.rotation;
    //        }
    //        else if (_cam6)
    //        {
    //            freecamPos.transform.position = vcam6.transform.position;
    //            freecam.transform.rotation = vcam6.transform.rotation;
    //        }
            
            
    //        vcam1.Priority = 0;
    //        vcam2.Priority = 0;
    //        vcam3.Priority = 0;
    //        vcam4.Priority = 0;
    //        vcam5.Priority = 0;
    //        vcam6.Priority = 0;
    //        freecam.Priority = 1;
            
    //        _cam1 = false;
    //        _cam2 = false;
    //        _cam3 = false;
    //        _cam4 = false;
    //        _cam5 = false;
    //        _cam6 = false;
    //        _freecam = true;
    //    }
        
    //}

    //[Button]
    //public void SetCamera1toFreeCam()
    //{
    //    cam1Pos.transform.position = freecamPos.transform.position;
    //    vcam1.transform.rotation = freecam.transform.rotation;
    //}
    
    //[Button]
    //public void SetCamera2toFreeCam()
    //{
    //    cam2Pos.transform.position = freecamPos.transform.position;
    //    vcam2.transform.rotation = freecam.transform.rotation;
    //}
    
    //[Button]
    //public void SetCamera3toFreeCam()
    //{
    //    cam3Pos.transform.position = freecamPos.transform.position;
    //    vcam3.transform.rotation = freecam.transform.rotation;
    //}
    
    //[Button]
    //public void SetCamera4toFreeCam()
    //{
    //    cam4Pos.transform.position = freecamPos.transform.position;
    //    vcam4.transform.rotation = freecam.transform.rotation;
    //}
    
    //[Button]
    //public void SetCamera5toFreeCam()
    //{
    //    cam5Pos.transform.position = freecamPos.transform.position;
    //    vcam5.transform.rotation = freecam.transform.rotation;
    //}
    
    //[Button]
    //public void SetCamera6toFreeCam()
    //{
    //    cam6Pos.transform.position = freecamPos.transform.position;
    //    vcam6.transform.rotation = freecam.transform.rotation;
    //}
}
