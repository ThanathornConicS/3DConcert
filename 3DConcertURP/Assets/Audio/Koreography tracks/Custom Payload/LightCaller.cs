using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightPosition
{
    all,
    left,
    right,
    Wide, 
    Narrow    
}

public enum LightAction
{
    intensity,
    front_back,
    underwater, 
    middle_wave,
    dimming_pattern, //for lasers
    underwater_to_halfBright
}

[CreateAssetMenu]
public class LightCaller : ScriptableObject
{
    /// <summary>
    /// Animation curve for animating lights
    /// </summary>

    [SerializeField]
    private LightPosition m_callLight;

    [SerializeField]
    private LightAction m_lightFuction;
    
    public LightPosition CallLight
    {
        get { return m_callLight; }
    }

    public LightAction LightFunction
    {
        get { return m_lightFuction; }
    }
}
