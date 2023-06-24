using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public class Cam1Pos : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam1;

    void Start()
    {
        this.transform.position = vcam1.transform.position;
        this.transform.rotation = vcam1.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = vcam1.transform.position;
        this.transform.rotation = vcam1.transform.rotation;
    }
}
