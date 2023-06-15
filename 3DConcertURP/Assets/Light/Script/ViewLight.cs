using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ViewLight : MonoBehaviour
{
    [SerializeField] private Renderer ObjectToChange;
    [SerializeField] private Material emissiveMaterial;

    [SerializeField] private int lightNumber;

    [Range(0.0f, 10.0f)]
    [SerializeField] private float defaultIntensity = 0.5f;

    private Color color;

    private bool turnOff = false;
    private float Intensity;

    // parameter for rainbow wave and fade between three color
    // TO-DO: Don't forfet to add speed for this two
    private int colorIndex = 0;
    private float t = 0.0f;
    private float allLightNumber = 10.0f;

    private bool startFade = true;
    private bool startRainbowWave = false;

    Color[] rainbow = { new Color(0, 0, 1), new Color(0, 1, 0.5f),
        new Color(0, 1, 0), new Color(0.5f, 0.5f, 0), new Color(1, 0, 0)};

    // Start is called before the first frame update
    private void Start()
    {
        emissiveMaterial = ObjectToChange.GetComponent<Renderer>().material;
        color = emissiveMaterial.GetColor("_EmissionColor");

        if(lightNumber % 2 == 0)
        {
            turnOff = true;
        }

        t = lightNumber / allLightNumber;
    }
    
    // Update is called once per frame
    void Update()
    {
        EmissionIntensity();
    }

    public void TurnEmissionOff()
    {
        emissiveMaterial.DisableKeyword("_EMISSION");
    }

    public void TurnEmissionOn()
    {
        emissiveMaterial.EnableKeyword("_EMISSION");
    }

    public void EmissionIntensity()
    {
        emissiveMaterial.SetColor("_EmissionColor", color * defaultIntensity);
    }

    public void ChangeColor(Color lightColor)
    {
        color = new Color(lightColor.r, lightColor.g, lightColor.b, defaultIntensity);
        emissiveMaterial.SetColor("_EmissionColor", color * defaultIntensity);
    }

    public void ChangeIntensity(float intensity)
    {
        defaultIntensity = intensity;
    }

    public void LightSwitch()
    {
        if(turnOff == false)
        {
            TurnEmissionOn();
            turnOff = true;
        }
        else
        {
            TurnEmissionOff();
            turnOff = false;
        }
    }

    public void RainbowWave(float lerpTime)
    {
        if(startRainbowWave == true)
        {
            colorIndex = 0;
            t = lightNumber / allLightNumber;

            startRainbowWave = false;
        }

        t = Mathf.Lerp(t, 1.0f, lerpTime * Time.deltaTime);
        if (t >= 0.9f)
        {
            colorIndex++;
            colorIndex = (colorIndex >= rainbow.Length) ? 0 : colorIndex;
            t = 0.0f;
        }

        Color ret = Color.Lerp(color, rainbow[colorIndex], lerpTime * Time.deltaTime);
        ChangeColor(ret);
    }

    public void FadeBetweenThreeColor(Color[] fadeColor, float lerpTime)
    {
        //colorIndex = (colorIndex >= fadeColor.Length) ? 0 : colorIndex;

        if(startFade == true)
        {
            colorIndex = 0;
            t = lightNumber / allLightNumber;

            startFade = false;
        }

        t = Mathf.Lerp(t, 1.0f, lerpTime * Time.deltaTime);
        if (t >= 0.9f)
        {
            colorIndex++;
            colorIndex = (colorIndex >= fadeColor.Length) ? 0 : colorIndex;
            t = 0.0f;
        }
        
        Color ret = Color.Lerp(color, fadeColor[colorIndex], lerpTime * Time.deltaTime);
        ChangeColor(ret);
    }

    public void SetLightNumber(int num)
    {
        lightNumber = num;

        if (lightNumber % 2 == 0)
        {
            turnOff = true;
        }
        else
        {
            turnOff = false;
        }

        t = lightNumber / allLightNumber;
    }

    public void SetRestartFade()
    {
        startFade = true;
    }

    public void SetRestartRainbowWave()
    {
        startRainbowWave = true;
    }

    public void SetAllLightNumber(float num)
    {
        allLightNumber = num;
        t = lightNumber / allLightNumber;
    }
}
