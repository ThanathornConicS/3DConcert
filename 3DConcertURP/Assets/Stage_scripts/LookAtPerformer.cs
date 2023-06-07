using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPerformer : MonoBehaviour
{
    public Transform performerPos;

    public Transform [] LightDir;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(performerPos, Vector3.forward);

        for (int i = 0; i < LightDir.Length; i++)
        {
            LightDir[i].LookAt(performerPos, Vector3.forward);
        }
    }
}
