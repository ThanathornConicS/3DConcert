using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LightController : MonoBehaviour
{
    enum LightStage { none, onAndOff, rainbowWave, fadeBetweenThreeColor };

    [Header("Stage")] [SerializeField] LightStage currentStage;

    [SerializeField] private Color lightColor = new Color(1.0f, 1.0f, 1.0f);
    [SerializeField] private List<Light> allLight = new List<Light>();
    [SerializeField] private float speed = 0.5f;

    [Range(0.0f, 1.0f)] [SerializeField] private float lerpTime = 0.8f;
    [Range(0.0f, 10.0f)] [SerializeField] private float Intensity = 1.5f;
    [SerializeField] private Color[] fadeColor;

    private float timer;
    private float previousIntensity;
    private Color previousColor;

    // Start is called before the first frame update
    void Start()
    {
        currentStage = LightStage.none;
    }

    // Update is called once per frame
    void Update()
    {
        SetAllColor();
        if(currentStage == LightStage.onAndOff)
        {
            OnAndOff();
        }
        else if(currentStage == LightStage.rainbowWave)
        {
            for(int i = 0; i < allLight.Count; i++)
            {
                allLight[i].RainbowWave(lerpTime);
            }
        }
        else if(currentStage == LightStage.fadeBetweenThreeColor)
        {
            for(int i = 0; i < allLight.Count; i++)
            {
                allLight[i].FadeBetweenThreeColor(fadeColor, lerpTime);
            }
        }

        if (previousIntensity != Intensity)
        {
            ChangeIntensity();
        }
    }

    [Button("Light Off")]
    void LightOff()
    {
        for(int i = 0; i < allLight.Count; i++)
        {
            allLight[i].TurnEmissionOff();
        }
        currentStage = LightStage.none;
    }

    [Button("Light On")]
    void LightOn()
    {
        for (int i = 0; i < allLight.Count; i++)
        {
            allLight[i].TurnEmissionOn();
        }
        currentStage = LightStage.none;
    }

    void ChangeIntensity()
    {
        for (int i = 0; i < allLight.Count; i++)
        {
            allLight[i].ChangeIntensity(Intensity);
        }
        previousIntensity = Intensity;
    }
    void SetAllColor()
    {
        if(previousColor.r != lightColor.r || previousColor.g != lightColor.g || previousColor.b != lightColor.b)
        {
            previousColor = new Color(lightColor.r, lightColor.g, lightColor.b);
            for(int i = 0; i < allLight.Count; i++)
            {
                allLight[i].ChangeColor(previousColor);
            }
            Debug.Log("Change Color");
        }
    }
    void OnAndOff()
    {
        timer += Time.deltaTime;

        if(timer >= speed)
        {
            for(int i = 0; i < allLight.Count; i++)
            {
                allLight[i].LightSwitch();
            }
            timer = 0.0f;
        }
    }
}