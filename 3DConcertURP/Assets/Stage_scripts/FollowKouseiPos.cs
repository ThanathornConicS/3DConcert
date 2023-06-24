using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowKouseiPos : MonoBehaviour
{
    [SerializeField]
    Transform hip;

    Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        pos.x = hip.position.x;
        pos.z = hip.position.z;
        pos.y = this.transform.position.y;

        this.transform.position = pos;
    }
}
