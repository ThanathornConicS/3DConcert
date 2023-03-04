using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPerformer : MonoBehaviour
{
    public Transform performerPos;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(performerPos, Vector3.forward);
    }
}
