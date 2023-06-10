using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MagicCircle : MonoBehaviour
{
    [SerializeField]
    Transform performer;

    [Button]
    void AttachMagicCircle()
    {
        performer.SetParent(this.transform, true);
    }

    [Button]
    void DetatchMagicCircle()
    {
        performer.transform.parent = null;
    }
    
}
