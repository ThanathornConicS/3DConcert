using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BackRow_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject[] WideSpotlights;
        
    [SerializeField]
    public GameObject[] NarrowSpotlights;

    private Vector3 OriginalRot;

    //Maxs Mins
    [SerializeField]
    private float WideIntensityMax, NarrowIntensityMax;

    void Start()
    {
        //Koreographer.Instance.RegisterForEventsWithTime(eventIntensity, UnderwaterLight);
        //Koreographer.Instance.RegisterForEventsWithTime(eventRotation, UnderwaterLight);
        OriginalRot = new Vector3(0.0f, 0.0f, 0.0f);
        ResetAngles();

        //Row 1
        WideIntensityMax = 1.8f;
        //Row 2
        NarrowIntensityMax = 2.0f;
    }

    [Button]
    private void ResetAngles()
    {
        for (int i = 0; i < WideSpotlights.Length; i++)
            WideSpotlights[i].transform.localEulerAngles = OriginalRot;

        for (int j = 0; j < NarrowSpotlights.Length; j++)
            NarrowSpotlights[j].transform.localEulerAngles = OriginalRot;
    }

    [Button]
    private void ResetIntensity()
    {
        for (int i = 0; i < WideSpotlights.Length; i++)
            ChangeIntensityWide(i, 1.0f);

        for (int j = 0; j < NarrowSpotlights.Length; j++)
            ChangeIntensityNarrow(j, 1.0f);
    }

    void ChangeIntensityWide(int i, float intensity) //intensity b/w 0-1
    {
        float val = Mathf.Lerp(0.0f, WideIntensityMax, intensity);
        WideSpotlights[i].GetComponent<Light>().intensity = val;
    }

    void ChangeIntensityNarrow(int i, float intensity) //intensity b/w 0-1
    {
        float val = Mathf.Lerp(0.0f, NarrowIntensityMax, intensity);
        NarrowSpotlights[i].GetComponent<Light>().intensity = val;
    }
}